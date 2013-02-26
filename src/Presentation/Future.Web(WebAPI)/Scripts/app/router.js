define(function (require) {
    var
        system = require('system'),
        viewModel = require('viewModel'),
        routesByPath = {},
        router,
        sammy,
        cancelling = false,
        activeItem = viewModel.activator(),
        activeRoute = ko.observable(),
        previousModule,
        previousRoute,
        navigationDefaultRoute,
        ready = ko.observable(false),
        isNavigating = ko.observable(false),
        allRoutes = ko.observableArray([]),
        visibleRoutes = ko.observableArray([]),
        getActivatableInstance = function (routeInfo, params, module) {
            if (typeof module == 'function') {
                return new module();
            } else {
                return module;
            }
        },
        shouldStopNavigation = function () {
            return cancelling || (sammy.last_location[1].replace('/', '') == previousRoute);
        },
        tryActivateRouter = function () {
            tryActivateRouter = system.noop;
            ready(true);
            router.dfd.resolve();
            delete router.dfd;
        },
        cancelNavigation = function () {
            cancelling = true;
            system.log('Cancelling Navigation');

            if (previousRoute) {
                sammy.setLocation(previousRoute);
            }

            cancelling = false;
            isNavigating(false);

            var routeAttempted = sammy.last_location[1].split('#/')[1];

            if (previousRoute || !routeAttempted) {
                tryActivateRouter();
            } else if (routeAttempted != navigationDefaultRoute) {
                window.location.replace("#/" + navigationDefaultRoute);
            } else {
                tryActivateRouter();
            }
        },
        onNavigationComplete = function (routeInfo, params, module) {
            //if (app.title) {
            //    document.title = routeInfo.name + " | " + app.title;
            //} else {
            //    document.title = routeInfo.name;
            //}
        },
        completeNavigation = function (routeInfo, params, module) {
            activeRoute(routeInfo);
            onNavigationComplete(routeInfo, params, module);
            previousModule = module;
            previousRoute = sammy.last_location[1].replace('/', '');
            tryActivateRouter();
        },
        activateRoute = function (routeInfo, params, module) {
            params.routeInfo = routeInfo;
            params.router = router;

            system.log('Activating Route', routeInfo, module, params);

            activeItem.activateItem(module, params).then(function (succeeded) {
                if (succeeded) {
                    completeNavigation(routeInfo, params, module);
                } else {
                    cancelNavigation();
                }
            });
        },
        ensureRoute = function (route, params) {
            var routeInfo = routesByPath[route];

            if (shouldStopNavigation()) {
                return;
            }

            if (!routeInfo) {
                if (!router.autoConvertRouteToModuleId) {
                    router.handleInvalidRoute(route, params);
                    return;
                }

                routeInfo = {
                    moduleId: router.autoConvertRouteToModuleId(route, params),
                    name: router.convertRouteToName(route)
                };
            }

            isNavigating(true);

            system.acquire(routeInfo.moduleId).then(function (module) {
                var instance = getActivatableInstance(routeInfo, params, module);
                activateRoute(routeInfo, params, instance);
            });
        },
        handleDefaultRoute = function () {
            ensureRoute(navigationDefaultRoute, this.params || {});
        },
        handleMappedRoute = function () {
            ensureRoute(this.app.last_route.path.toString(), this.params || {});
        },
        handleWildCardRoute = function () {
            var params = this.params || {}, route;

            if (router.autoConvertRouteToModuleId) {
                var fragment = this.path.split('#/');

                if (fragment.length == 2) {
                    var parts = fragment[1].split('/');
                    route = parts[0];
                    params.splat = parts.splice(1);
                    ensureRoute(route, params);
                    return;
                }
            }

            router.handleInvalidRoute(this.app.last_location[1], params);
        },
        activate = function (defaultRoute) {
            return $.Deferred(function (dfd) {
                var processedRoute;

                router.dfd = dfd;
                navigationDefaultRoute = defaultRoute;

                sammy = Sammy(function (route) {
                    var unwrapped = allRoutes();

                    for (var i = 0; i < unwrapped.length; i++) {
                        var current = unwrapped[i];
                        route.get(current.url, handleMappedRoute);
                        processedRoute = this.routes.get[i];
                        routesByPath[processedRoute.path.toString()] = current;
                    }

                    route.get(/\#\/(.*)/, handleWildCardRoute);
                    route.get('', handleDefaultRoute);
                });

                sammy._checkFormSubmission = function () {
                    return false;
                };

                sammy.log = function () {
                    var args = Array.prototype.slice.call(arguments, 0);
                    args.unshift('Sammy');
                    system.log.apply(system, args);
                };

                sammy.run();
            }).promise();
        },
        stripParameter = function (val) {
            var colonIndex = val.indexOf(':');
            var length = colonIndex > 0 ? colonIndex - 1 : val.length;
            return val.substring(0, length);
        },
        convertRouteToName = function (route) {
            var value = stripParameter(route);
            return value.substring(0, 1).toUpperCase() + value.substring(1);
        },
        convertRouteToModuleId = function (route) {
            return stripParameter(route);
        },
        prepareRouteInfo = function (info) {
            if (!(info.url instanceof RegExp)) {
                info.name = info.name || convertRouteToName(info.url);
                info.moduleId = info.moduleId || convertRouteToModuleId(info.url);
                info.hash = info.hash || '#/' + info.url;
            }

            info.caption = info.caption || info.name;
            info.settings = info.settings || {};
        },
        configureRoute = function (routeInfo) {
            prepareRouteInfo(routeInfo);

            routesByPath[routeInfo.url] = routeInfo;
            allRoutes.push(routeInfo);

            if (routeInfo.visible) {
                routeInfo.isActive = ko.computed(function () {
                    return ready() && activeItem() && activeItem().__moduleId__ == routeInfo.moduleId;
                });

                visibleRoutes.push(routeInfo);
            }

            return routeInfo;
        },
        mapRoute = function (urlOrConfig, moduleId, name, visible) {
            if (typeof urlOrConfig == "string") {
                return configureRoute({
                    url: urlOrConfig,
                    moduleId: moduleId,
                    name: name,
                    visible: visible
                });
            } else {
                return configureRoute(urlOrConfig);
            }
        },
        mapNav = function (urlOrConfig, moduleId, name) {
            if (typeof urlOrConfig == "string") {
                return mapRoute(urlOrConfig, moduleId, name, true);
            }

            urlOrConfig.visible = true;
            return configureRoute(urlOrConfig);
        };

    return router = {
        isNavigating: isNavigating,
        visibleRoutes: visibleRoutes,
        mapNav: mapNav,
        activate: activate,
        activeItem: activeItem
    };
});
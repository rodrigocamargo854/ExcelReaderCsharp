import React from "react";
import { Router, Route, Switch } from "react-router-dom";

import * as Layout from "./views/layout/admin";
import * as Pages from "./views/pages/index";

import { PermissionTypeEnum } from "./application/routes/permissionTypes";
import listRoutes from "./application/routes/Routes";
import history from "~/application/routes/history";

import {
  RouteProperties,
  BroadcrumpProps,
} from "~/application/routes/interfaces";

export interface routesProps {
  routes: RouteProperties[];
  access: PermissionTypeEnum;
}

const Routes: React.FC = () => {
  let path = "";
  let list: { [index: string]: BroadcrumpProps[] } = {};
  let rotas: RouteProperties[] = [];
  let itemFather: BroadcrumpProps[] = [];

  function RouteAuth({ routes, access }: routesProps) {
    routes.forEach((route) => {
      list[path + route.path] = itemFather.filter(
        (x) => x.link === path || path + route.path
      );
      list[path + route.path].push({
        text: route.breadcrumbName,
        link: path + route.path,
      });
      route.breadcrumbList = list[path + route.path];
      rotas.push({ ...route, path: path + route.path });
      if (route.children !== undefined) {
        path = path + route.path;
        itemFather.push({ text: route.breadcrumbName, link: path });
        return RouteAuth({
          routes: route.children,
          access,
        });
      }
    });

    return rotas;
  }

  return (
    <Router history= {history}>
      <Switch>
        <Route path="/login" component={Pages.Login} />
        {RouteAuth({
          routes: listRoutes,
          access: PermissionTypeEnum.admin,
        }).map((route, index) => {
          return (
            <Route
              key={index}
              path={route.path}
              render={() => {
                return (
                  <Layout.Main>
                    <Layout.Page
                      component={route.component}
                      broadcrumb={route.breadcrumbList}
                    />
                  </Layout.Main>
                );
              }}
              exact={route.exact}
            />
          );
        })}
      </Switch>
    </Router>
  );
};

export default Routes;

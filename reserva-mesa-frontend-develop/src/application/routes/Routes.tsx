import * as Page from "~/views/pages";
import { RouteProperties } from "./interfaces";
import { PermissionTypeEnum } from "./permissionTypes";

const routes: RouteProperties[] = [
  {
    permission: [PermissionTypeEnum.admin, PermissionTypeEnum.user],
    component: Page.Home,
    exact: true,
    breadcrumbName: "Home",
    path: "/",
  },
  {
    permission: [PermissionTypeEnum.admin, PermissionTypeEnum.user],
    component: Page.Erros.Erro500,
    exact: true,
    breadcrumbName: "Erro no servidor",
    path: "/error-500",
  },
  {
    permission: [PermissionTypeEnum.admin, PermissionTypeEnum.user],
    component: Page.CreateReservation,
    exact: true,
    breadcrumbName: "Reservar Mesa",
    path: "/create-reservation",
  },
  {
    permission: [PermissionTypeEnum.admin, PermissionTypeEnum.user],
    component: Page.Reservations,
    exact: true,
    breadcrumbName: "Minhas reservas",
    path: "/my-reservations",
  },
  {
    permission: [PermissionTypeEnum.receptionist],
    component: Page.CheckIn,
    exact: true,
    breadcrumbName: "Check In",
    path: "/check-in",
  },
  {
    permission: [PermissionTypeEnum.admin, PermissionTypeEnum.user],
    component: Page.FullscreenMap,
    exact: true,
    breadcrumbName: "Mapa em tela cheia",
    path: "/fullscreen-map",
  },
  {
    permission: [PermissionTypeEnum.admin],
    component: Page.Reports,
    exact: true,
    breadcrumbName: "Relatórios",
    path: "/reports",
  },
  {
    permission: [PermissionTypeEnum.admin],
    component: Page.Floors,
    exact: true,
    breadcrumbName: "Alterar andares",
    path: "/floors",
  },
  {
    permission: [PermissionTypeEnum.admin],
    component: Page.AdminMap,
    exact: true,
    breadcrumbName: "Alterar estações de trabalho",
    path: "/update-workstations",
  },
];

export default routes;

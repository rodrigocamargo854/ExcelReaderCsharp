import {PermissionTypeEnum} from "./routes/permissionTypes";
import {MenuProperties} from "~/application/routes/interfaces";

const Menu: MenuProperties[] = [
  {
    permission: [PermissionTypeEnum.admin, PermissionTypeEnum.user],
    key: "reservations",
    path: "/my-reservations",
    icon: "UnorderedListOutlined",
    label: "Minhas Reservas",
  },
  {
    permission: [PermissionTypeEnum.admin, PermissionTypeEnum.user],
    key: "create-reservation",
    path: "/create-reservation",
    icon: "FormOutlined",
    label: "Reservar mesa",
  },
  {
    permission: [PermissionTypeEnum.receptionist],
    key: "check-in",
    path: "/check-in",
    icon: "CheckOutlined",
    label: "Check In",
  },
  {
    permission: [PermissionTypeEnum.admin],
    key: "reports",
    path: "/reports",
    icon: "LineChartOutlined",
    label: "Relatórios",
  },
  {
    permission: [PermissionTypeEnum.admin],
    key: "floors",
    path: "/floors",
    icon: "InsertRowLeftOutlined",
    label: "Alterar andares",
  },
  {
    permission: [PermissionTypeEnum.admin],
    key: "admin-map",
    path: "/update-workstations",
    icon: "AppstoreOutlined",
    label: "Alterar estações de trabalho",
  }
];

export default Menu;

import React, { useContext } from "react";
import { useHistory } from "react-router-dom";
import { Menu } from "antd";
import "./index.scss";
import { Icon } from "~/components";
import { ApplicationContext } from "~/contexts";
import { PermissionTypeEnum } from "~/application/routes/permissionTypes";
import { MenuProperties } from "~/application/routes/interfaces";

interface MenuProps {
  mode?:
    | "inline"
    | "horizontal"
    | "vertical"
    | "vertical-left"
    | "vertical-right";
  data?: MenuProperties[];
}

const ListMenu = (props: MenuProps) => {
  const context = useContext(ApplicationContext);
  const history = useHistory();

  const handleClick = (path: string) => {
    context.setLoad(true);
    history.push(path);
  };

  const listMenu = props.data;

  const convertUserRolesToPermissionEnum = (): PermissionTypeEnum[] => {
    const permissions = [PermissionTypeEnum.user];

    if (context.authenticatedUserRoles.isAdmin) {
      permissions.push(PermissionTypeEnum.admin);
    }
    if (context.authenticatedUserRoles.isReceptionist) {
      permissions.push(PermissionTypeEnum.receptionist);
    }

    return permissions;
  }

  const verifyPermission = (rowPermissions: PermissionTypeEnum[]): boolean => {
    let hasPermission = false;
    const permissions = convertUserRolesToPermissionEnum();

    permissions.forEach(permission => {
      if (Object.values(rowPermissions).includes(permission)) {
        hasPermission = true;
      }
    });
    return hasPermission;
  }

  return (
    <Menu
      className="sidebar-left bg_dark"
      key={context.activeMenu}
      {...props}
      defaultSelectedKeys={[context.activeMenu]}
      defaultOpenKeys={[context.activeMenu]}
    >
      {listMenu?.map((row) => {
        return (
          <Menu.Item
            onClick={() => handleClick(row.path)}
            key={row.key}
            icon={<Icon name={row.icon} />}
            hidden={!verifyPermission(row.permission)}
          >
            {row.label}
          </Menu.Item>
        );
      })}
    </Menu>
  );
};

export default ListMenu;

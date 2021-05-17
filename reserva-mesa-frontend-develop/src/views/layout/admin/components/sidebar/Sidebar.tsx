import React, { useContext } from "react";
import { Layout, Drawer } from "antd";
import { ReactComponent as MiniLogo } from "assets/mini-logo.svg";
import { ReactComponent as Logo } from "assets/logo.svg";
import "./index.scss";
import { ApplicationContext } from "~/contexts";
import { ListMenu } from "../";
import {Menu} from "~/application";

const { Sider } = Layout;

const Sidebar = () => {
  const applicationContext = useContext(ApplicationContext);

  return (
    <>
      <Drawer
        title={<Logo />}
        placement="left"
        closable={false}
        onClose={() => applicationContext.handleMenu()}
        visible={applicationContext.collapsed}
        className="show-mobile"
      >
        <ListMenu mode="inline" data={Menu} />
      </Drawer>

      <Sider
        collapsible
        collapsed={!applicationContext.collapsed}
        onCollapse={applicationContext.handleMenu}
        className="sidebar-left show-web bg_dark"
      >
        <div className="logo">
          {!applicationContext.collapsed ? <MiniLogo /> : <Logo />}
        </div>
        <ListMenu data={Menu} />
      </Sider>
    </>
  );
};

export default Sidebar;

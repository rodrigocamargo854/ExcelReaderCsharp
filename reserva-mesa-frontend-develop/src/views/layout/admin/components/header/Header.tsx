import React, { useContext } from "react";
import { Layout } from "antd";
import "./index.scss";
import { ApplicationContext } from "~/contexts";
import { Application } from "~/application";

import { LogoutOutlined, MenuOutlined } from "@ant-design/icons";

import { auth } from "~/services"

const Header: React.FC = (props) => {
  const applicationContext = useContext(ApplicationContext);

  const { Header } = Layout;

  function handleLogout() {
    auth.logout();
  }

  return (
    <Header>
      <div>
        {React.createElement(MenuOutlined, {
          className: "trigger show-mobile",
          onClick: () => applicationContext.setCollapsed(true),
        })}

        <span className="title-header">{Application.title}</span>
      </div>
      <div>
        <LogoutOutlined className="ico-logout-righ" onClick={handleLogout} />
      </div>
    </Header>
  );
};

export default Header;

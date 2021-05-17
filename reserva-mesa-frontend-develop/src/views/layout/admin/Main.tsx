import React, { ReactNode, useEffect, useContext } from "react";
import { Layout, Spin } from "antd";
import { Header, Sidebar, Breadcrumb } from "./components";
import { ApplicationContext } from "~/contexts";
import "./main.scss";

const { Content } = Layout;

export interface LayoutProps {
  children: ReactNode;
}

function Main({ children }: LayoutProps) {
  const context = useContext(ApplicationContext);

  useEffect(() => {  
    setTimeout(() => {
      context.setLoad(false);
    }, 1000);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [context.load]);

  return (
    <Layout className="container">
      <Sidebar />
      <Layout>
        <Header />
        <Layout className="content">
          <Spin spinning={context.load}>
            <div style={{ display: context.load ? "none" : "initial" }}>
              <Breadcrumb />
              <Content className="page">{children}</Content>
            </div>
          </Spin>
        </Layout>
      </Layout>
    </Layout>
  );
}

export default Main;

import React, { useContext, useEffect } from "react";
import { Link } from "react-router-dom";
import { Breadcrumb as Layout } from "antd";
import { BroadcrumpProps } from "~/application/routes/interfaces";
import { ApplicationContext } from "~/contexts";

export interface BoradcrumpProps {
  broadcrumb?: BroadcrumpProps[] | undefined;
}

const Breadcrumb: React.FC = () => {
  const context = useContext(ApplicationContext);
  useEffect(() => {
    context.setLoad(true);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [context.breadcrumb]);

  return (
    <Layout>
      {context.breadcrumb?.map((item, index) => (
        <Layout.Item key={index}>
          {item.link === undefined ? (
            item.text
          ) : (
            <Link to={item.link}>{item.text}</Link>
          )}
        </Layout.Item>
      ))}
    </Layout>
  );
};

export default Breadcrumb;

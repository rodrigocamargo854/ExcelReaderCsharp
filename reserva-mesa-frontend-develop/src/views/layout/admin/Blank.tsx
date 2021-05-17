import React, {ReactNode} from "react";
import { Layout } from "antd";
import "./main.scss";

export interface LayoutProps  { 
  children: ReactNode;
}

function Blank({children}: LayoutProps){
  return (
      <Layout className="container">
        { children }
      </Layout>
  );
};

export default Blank;

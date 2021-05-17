import React, { useContext } from "react";
import { Layout} from "antd";
import { ApplicationContext } from "../../../contexts";


const Login: React.FC = () => {
  const context = useContext(ApplicationContext);

  context.setLoad(false);

  return (
    <Layout className="login">
      login aqui
    </Layout>
  );
};

export default Login;

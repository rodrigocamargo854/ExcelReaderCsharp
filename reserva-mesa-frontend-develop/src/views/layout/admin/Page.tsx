import React, { useEffect, useContext } from "react";
import { ApplicationContext } from "~/contexts";
import { BroadcrumpProps } from "~/application/routes/interfaces";
import { getUserRoles } from "~/services/http/backend/user-service";

export interface LayoutProps {
  component: React.FC<{}>;
  broadcrumb?: BroadcrumpProps[] | undefined;
}

const Page = ({ component, broadcrumb }: LayoutProps) => {
  const context = useContext(ApplicationContext);
  useEffect(() => {
    if (broadcrumb !== undefined) {
      context.sendBreadcrumb(broadcrumb);
    }

    (async () => {
      const response = await getUserRoles();
      context.setAuthenticatedUserRoles(response.data);
    })();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const Component = component;
  return <Component />;
};

export default Page;

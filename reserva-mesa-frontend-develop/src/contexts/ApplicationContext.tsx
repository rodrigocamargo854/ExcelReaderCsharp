import React, {
  createContext,
  useState,
  Dispatch,
  SetStateAction
} from "react";
import { UserRoles } from "~/domain/models/user";

interface BroadcrumpProps{
  text: string | undefined;
  link?: string | undefined;
}
interface Admin {
  collapsed?: boolean;
  handleMenu: () => void;
  setCollapsed: Dispatch<SetStateAction<boolean>>;
  load: boolean;
  setLoad: Dispatch<SetStateAction<boolean>>;
  breadcrumb?: BroadcrumpProps[] | undefined;
  sendBreadcrumb: (props: BroadcrumpProps[]) => void;
  activeMenu: string;
  setActiveMenu: Dispatch<SetStateAction<string>>;
  authenticatedUserRoles: UserRoles;
  setAuthenticatedUserRoles: (value: UserRoles) => void;
}

const ApplicationContext = createContext<Admin>({} as Admin);
const ApplicationProvider: React.FC = ({ children }) => {
  const [collapsed, setCollapsed] = useState(false);
  const [breadcrumb, setBreadcrumb] = useState<BroadcrumpProps[]>();
  const [activeMenu, setActiveMenu] = useState<string>("");
  const [load, setLoad] = useState<boolean>(false);
  const [authenticatedUserRoles, setAuthenticatedUserRoles] = useState<UserRoles>({ isAdmin: false, isReceptionist: false });

  const handleMenu = () => {
    setCollapsed(!collapsed);
  };

  const sendBreadcrumb = (props: BroadcrumpProps[]) => {
    setBreadcrumb(props)
  }

  return (
    <ApplicationContext.Provider
      value={{
        collapsed,
        setCollapsed,
        handleMenu,
        load,
        setLoad,
        breadcrumb,
        sendBreadcrumb,
        activeMenu,
        setActiveMenu,
        authenticatedUserRoles,
        setAuthenticatedUserRoles
      }}
    >
      {children}
    </ApplicationContext.Provider>
  );
};

export { ApplicationContext, ApplicationProvider };

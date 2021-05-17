import { useContext, useEffect } from "react";
import { ApplicationContext } from "../../contexts";

export function _setActiveMenu(key: string) {
  const { setActiveMenu } = useContext(ApplicationContext);

  useEffect(() => {
    setActiveMenu(key);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
}

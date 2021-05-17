import { AdalConfig, AuthenticationContext, withAdalLogin } from "react-adal";
import adalconfig from "~/config/adalConfig";

const endpointApi = adalconfig.endpoint_api || "";
const configuration: AdalConfig = {
  tenant: adalconfig.tenant,
  clientId: adalconfig.client_id || "",
  redirectUri: adalconfig.redirect_uri,
  endpoints: {
    api: endpointApi
  },
  cacheLocation: "sessionStorage",
}
export const authContext = new AuthenticationContext(configuration);

export const getToken = (): string | null => {
  return authContext.getCachedToken(configuration.clientId);
}

export const isAuthenticated = (): boolean => {
  const token = authContext.getCachedToken(configuration.clientId);
  return !!token;
}

export const logout = (): void => {
  authContext.logOut();
}

export const withAdalLoginApi = withAdalLogin(authContext, endpointApi);
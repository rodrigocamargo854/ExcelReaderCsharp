import React, {createContext} from "react";
import useAuth from "../hooks/useAuth";


const AuthContext = createContext();

const AuthProvider = ({ children }) => {
  const {isAuthenticated, login} = useAuth();
  
  return(
    <AuthContext.Provider value={{isAuthenticated, login} }>
      {children}
    </AuthContext.Provider>
  )
};

export {AuthContext, AuthProvider}
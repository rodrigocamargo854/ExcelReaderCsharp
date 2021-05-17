import jwtDecode from "jwt-decode";
import { useState } from "react";
import { useEffectOnce } from "react-use";
import { api } from "../../services/api";

const TOKEN_USER = "token";
const USER = "user";

export default function useAuth() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [loading, setLoading] = useState(true);
  const [user, setUser] = useState();

  useEffectOnce(() => {
    async function loadStorageData() {
      const tokenlocalStorage = localStorage.getItem(TOKEN_USER);
           
      if (tokenlocalStorage) {
        const user = decodeTokenToGetUserInformations();
        setUser(user);
        api.defaults.headers.Authorization = `Bearer ${tokenlocalStorage}`;
        api.defaults.headers.UserId = user.userId;
        setIsAuthenticated(true);
      }
      setLoading(false);
    }
    loadStorageData();
  }, []);
  
  async function login({ email, password }) {
    const response = await api.post("/users/login", { email, password });
    localStorage.setItem(TOKEN_USER, response.data.token);
    decodeTokenToGetUserInformations();
    setIsAuthenticated(true);
  }

  async function signOut() {
    localStorage.clear();
    setUser(null);
    setIsAuthenticated(false);
  }

  function decodeTokenToGetUserInformations(){
     const decodeToken = jwtDecode(localStorage.getItem(TOKEN_USER));
     
     const user={
       name: decodeToken.unique_name,
       email: decodeToken.email,
       userId: decodeToken.nameid,
       cellphone: Object.values(decodeToken)[3]       
     }
     return user;
  }
  
  return { isAuthenticated, login, user, signOut, loading };
}

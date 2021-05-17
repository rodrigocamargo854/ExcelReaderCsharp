import "bootstrap/dist/css/bootstrap.min.css";
import "./index.css";
import React from "react";
import Login from "./Components/Login/Login.jsx";
import SignUp from "./Components/FormRegister/SignUp.jsx";
import {
  BrowserRouter,
  Switch,
  Route
} from "react-router-dom";
import NotFound from "../src/UI/Pages/NotFound/NotFound";
import MainPage from "./UI/Pages/MainPage";
import Reports from "./Components/Reports/Reports.jsx";
import PriceAverage from "./Components/PriceAverage/PriceAverage.jsx";
import ProdutoEscolhido from "./UI/Pages/ProdutoEscolhido";
import MainPageProfile from "./Components/PageProfile/MainPageProfile"

export default function Routes() {
  return (
    //configurando a autenticação para obter as rotas
    <BrowserRouter>
      <Switch>
    
        <Route exact path="/" component={MainPage} />
        <Route path="/Login" component={Login} />

        <Route path="/SignUp" component={SignUp} />
        <Route path="/Reports" component={Reports} />
  
        <Route path="/ProdutosPesquisados"><PriceAverage /></Route>
        <Route
          path="/ProdutoEscolhido/:id"
          children={<ProdutoEscolhido />}
        />
        <Route path="/MainPageProfile" component={MainPageProfile} />

        <Route path="*" component={NotFound} />
      </Switch>
    </BrowserRouter>
  );
}

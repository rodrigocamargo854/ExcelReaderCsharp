import React from "react";
import ImageError500 from "~/assets/error-500.png";

export const Unauthorized: React.FC<{}> = () => {
  return (
    <>
      <h1>Erro 401</h1>
      <p>Você não tem permissão para acessar essa página.</p>
    </>
  );
};

export const Erro404: React.FC<{}> = () => {
  return (
    <>
      <h1>Erro 404</h1>
      <p>Página não encontrada.</p>
    </>
  );
};

export const Erro500: React.FC<{}> = () => {
  return (
    <>
      <h1>Erro 500</h1>
      <p>Ocorreu um erro de conexão com o servidor.</p>
      <img src={ImageError500} alt="error-500" height="200" width="250"/>
    </>
  );
};

import styled from "styled-components";
import imagem from "../assets/logoicone.png";
import { ButtonStyled } from '../../UI/Button/styles';



export const LoginButton = styled(ButtonStyled)`
  margin-left: 10rem;
  border: 2px solid #ff6633 !important;
  border-radius:8px;
  background-color: #fff;
  color: #ff6633;
  text-decoration: none;
  
  &:hover {
    background-color: #ff6633 ;
    color: #fff;
    opacity:100%;
    text-decoration: none !important;
    }

`;

export const LogOutButton = styled(ButtonStyled)`
  margin-left: 10rem;
  border: 2px solid #ff6633 !important;
  border-radius:8px;
  background-color: #fff;
  color: #ff6633;
  text-decoration: none;
  
  &:hover {
    background-color: #ff6633 ;
    color: #fff;
    opacity:100%;
    text-decoration: none !important;
    }
`;

export const SearchBarStyled = styled.div`
  width: 48rem;
  margin-left: -11rem;
  border-radius:8px;

`;
export const SigInButton = styled(ButtonStyled)`
  border: 2px solid #ff6633 !important;
  margin-left: 10px;
  border: 2px solid #ff6633 !important;
  border-radius:8px;
  background-color: #fff;
  color: #ff6633;
  text-decoration: none;
  
  &:hover {
    background-color: #ff6633 ;
    color: #fff;
    opacity:100%;
    text-decoration: none !important;
    }
`;

export const Imagem = styled(imagem)`
  max-width: 11rem;
`;

import styled from "styled-components";
import { ButtonStyled } from "../../../UI/Button/styles";

export const ButtonSearch = styled(ButtonStyled)`
  border: 2px solid #ff6633 !important;
  border-radius: 8px;
  background-color: #fff;
  color: #ff6633;
  text-decoration: none;

  &:hover {
    background-color: #ff6633;
    color: #fff;
    opacity: 100%;
    text-decoration: none !important;
  }
`;

export const FormControlStyled = styled.input`
  margin-left: 6rem;
  width: 60%;
  max-width: 60%;
  border-radius: 8px;
  font-size: 1rem;
  font-weight: 400;
  line-height: 1.5;
  color: #495057;
  background-color: #fff;
  background-clip: padding-box;
  border: 1px solid #f2f5f8;
  border-color: #f2f5f8;
  &:focus {
    border: none;
  }
`;

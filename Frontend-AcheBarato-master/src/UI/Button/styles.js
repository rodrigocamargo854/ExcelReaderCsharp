import {Button} from 'react-bootstrap';
import styled from 'styled-components';



export const ButtonStyled = styled(Button)`
display: flex;
margin-left: 1rem;

`;

export const GlobalButton = styled.button`
  color: #ff6633;
  background-color: rgba(255, 102, 51, 0);
  border: 2px solid #ff6633 !important;
  width: 150px;
  border-radius: 8px;
  height: 43px;
  border-color: #ff6633;
  opacity:70%;

  &:hover {
    background-color: #ff6633;
    color: #fff;
    opacity:100%;

  }
  &:focus {
    outline: 0;
  }
`;
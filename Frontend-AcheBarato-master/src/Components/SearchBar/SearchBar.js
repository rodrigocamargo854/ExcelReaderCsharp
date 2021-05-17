import styled from "styled-components";


export const ButtonStyled = styled.button`
  color: #ff6633;
  background-color: rgba(255, 102, 51, 0);
  border: 2px solid #ff6633 !important;
  width: 100px;
  border-radius: 8px;
  height: 43px;
  border-color: #ff6633;
  transition: width 1s;
  opacity:70%;
  margin-left:2rem;

  &:hover {
    background-color: #ff6633;
    color: #fff;
    opacity:100%;
    text-decoration: none;

    }
    
  &:link{
    text-decoration: none;
  }
  &:focus{
    outline: 0;
  }`;

export const FormControlStyled = styled.input`
display: flex;
margin-left: 1rem;
border-radius: 8px;
display: block;
width: 80%;
height: calc(1.5em + 0.75rem + 2px);
padding: .375rem .85rem;
font-size: 1rem;
font-weight: 400;
line-height: 1.5;
color: #495057;
background-color: #fff;
background-clip: padding-box;
border: 1px solid #f2f5f8;
border-color: #f2f5f8;
transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out;
outline: 0;

`;
import React from 'react'
import { Navbar, Nav, NavDropdown, Container, Row, Col,Image } from 'react-bootstrap';
import { LoginButton, SigInButton } from "./menustyles";
import imagem from "../assets/logoicone.png";
import { Link } from 'react-router-dom';
import CarouselProducts from '../CarouselProducts/CarouselProducts.jsx';
import SearchBar from '../SearchBar/SearchBar.jsx';

const Navbarmenu = () => (
<>
<div class="menu">
  <Navbar collapseOnSelect expand="xl" bg="dark" variant="dark">
    <Navbar.Brand href="#home">
    <img src={imagem} />
    </Navbar.Brand>
    <Navbar.Toggle aria-controls="responsive-navbar-nav" />
    <Navbar.Collapse id="responsive-navbar-nav">
      <Nav className="mr-auto">
        <Nav.Link href="#faf5f4tures">Outlet</Nav.Link>
        <Nav.Link href="#pricing">Best Offers</Nav.Link>
        <NavDropdown title="Cathegorys" id="collasible-nav-dropdown">
          <NavDropdown.Item href="#action/3.1">Categoria1</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.2">Categoria2</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.3">Categoria3</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.4">Categoria4</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.5">Categoria5</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.6">Categoria6</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.7">Categoria7</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.8">Categoria8</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.9">Categoria9</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.10">Categoria10</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.11">Categoria11</NavDropdown.Item>
          <NavDropdown.Item href="#action/3.12">Categoria12</NavDropdown.Item>
          <NavDropdown.Divider />
          <NavDropdown.Item href="#action/3.13">Pesquisa rápida</NavDropdown.Item>
        </NavDropdown>

      </Nav>

      <Nav>
        <div>
          
        </div>
        
        <div style={{display:"flex"}}>
          <Link to='/Login'>
          <LoginButton variant="secondary">Login</LoginButton>
          </Link>
          <Link to='/FormRegister'>
          <SigInButton variant="secondary">Register</SigInButton>
          </Link>
        </div>
      </Nav>
    </Navbar.Collapse>
  </Navbar>
  </div>
  
</>
)

export default Navbarmenu;
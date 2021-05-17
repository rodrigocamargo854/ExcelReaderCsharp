import React from "react";
import CarouselProdutos from "../../Components/CarouselProducts/CarouselProducts";
import CarouselMainPage from '../../Components/CarouselMainPage/CarouselMainPage';
import SearchBar from "../../Components/SearchBar/SearchBar.jsx"
import {JumbotronStyled} from "./MainPageStyles/mainpagesstyles";
import Footer from '../../Components/Footer/Footer.jsx';
import MenuSearchBar from "../../Components/MenuSearchBar/index";


function MainPage() {
  return (

    <>
      <div class="menu-bar">
        <MenuSearchBar />
      </div>
      <CarouselMainPage />
      <JumbotronStyled>
      <SearchBar/>
      </JumbotronStyled>
      <CarouselProdutos />
      
     <Footer/>
    </>
    
  );
}



export default MainPage;

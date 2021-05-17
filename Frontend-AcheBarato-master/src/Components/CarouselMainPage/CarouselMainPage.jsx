import React from "react";
import { Carousel } from "react-bootstrap";
import Styled from "styled-components";
import imagem1 from "../assets/000.png";
import imagem2 from "../assets/001.png";
import imagem3 from "../assets/002.png";


const CarouselStyled = Styled(Carousel)`
  margin-bottom: 100px;
  margin: flex;
`;

function CarouselMainPage() {
  

  return (
    <CarouselStyled>
      <Carousel.Item>
        <img
          height="360px"
          className="d-block w-100"
          src={imagem1}
          alt="First slide"
        />
      </Carousel.Item>
      <Carousel.Item>
        <img
          height="360px"
          className="d-block w-100"
          src={imagem2}
          alt="First slide"
        />
      </Carousel.Item>

      <Carousel.Item>
        <img
          height="360px"
          className="d-block w-100"
          src={imagem3}
          alt="First slide"
        />
      </Carousel.Item>

    </CarouselStyled>
  );
}

export default CarouselMainPage;

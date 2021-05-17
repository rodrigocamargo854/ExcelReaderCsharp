import React from "react";
import { Carousel } from "react-bootstrap";
import Styled from "styled-components";


const CarouselStyled = Styled(Carousel)`
  margin-bottom: 100px;
  margin: flex;
`;

function CarouselPictureSelected(props) {
  
  const carouselImagesItems = props.pictures.map((picture) => (
    <Carousel.Item>
      <img
        height="360px"
        width= "250px"
        className="d-block w-100"
        src={picture}
        alt="First slide"
      />
    </Carousel.Item>
  ));

  return (
    <CarouselStyled>
      {carouselImagesItems}
    </CarouselStyled>
  );
}

export default CarouselPictureSelected;

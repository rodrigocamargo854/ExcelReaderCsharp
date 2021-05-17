
import React from "react";
import { Card,CardGroup } from "react-bootstrap";
import CarouselProducts from "../CarouselProducts/CarouselProducts.jsx";
import {DivCard} from "../PriceAverage/PriceAverage.js";
import TechnicalInformation from '../TecnicalInformation/TechnicalInformation.jsx';

const CardResultProducts = () => {
  return (
    <>
<DivCard>
<CarouselProducts trendingProducts={[
        { name: "cyberpunk", description: "xsxs", category: "eletronicos" },
        { name: "shbah", description: "dsjbasj", category: "eletronicos" },
        { name: "ssjnjn", description: "kmdskmd", category: "eletronicos" },
        { name: "hbahasa", description: "dkdmskmd", category: "eletronicos" },
        { name: "hbahasa", description: "dkdmskmd", category: "roupas" },
        { name: "hbahasa", description: "dkdmskmd", category: "roupas" },
        { name: "hbahasa", description: "dkdmskmd", category: "roupas" },
        { name: "hbahasa", description: "dkdmskmd", category: "roupas" },
        { name: "hbahasa", description: "dkdmskmd", category: "computadores" },
        { name: "hbahasa", description: "dkdmskmd", category: "computadores" },
      ]} />
  
</DivCard>

</>
  );
};

export default CardResultProducts;

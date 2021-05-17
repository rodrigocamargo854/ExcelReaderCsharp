import React, {useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import { Button } from "../Button/index";
import { Accordion, Badge } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { getProductSelected } from "../../services/api.js"
import CardListProduct from "../../Components/CardListProduct/CardListProduct";
import TechnicalInformation from "../../Components/TecnicalInformation/TechnicalInformation";
import NavSuperior from "../../Components/MenuSearchBar/index";
import axios from "axios";
import PriceCharts from "../../Components/PriceAverage/Charts/PriceCharts";
import CarouselPictureSelected from "../../Components/CarouselPictureSelected/CarouselPictureSelected";
import {useEffectOnce} from "react-use";


const ProdutoEscolhido = () => {
  const [productSelected, setProductSelected] = useState([]);
  const [relatedProducts, setRelatedProducts] = useState([]);
  const [isLoaded, setIsLoaded] = useState(false);
  let { id } = useParams();

  useEffectOnce(() => {
    async function loadRelatedProducts() {
      await getProductSelected({ id })
        .then(
          axios.spread((ps, rp) => {
            setProductSelected(ps.data.data);
            setRelatedProducts(rp.data);
          })
        );

      setIsLoaded(true);
    }

    loadRelatedProducts();
  });

  const productRelatedPartOne = relatedProducts.slice(
    Math.max(relatedProducts.length - 5, 1)
  );
   
  return (
    <>
      <NavSuperior />
      <Container className="ml-auto mr-auto mt-3">
        {isLoaded && (
          <>
            <Row>
              {
                <>
                  <Col xs={6}>
                    <CarouselPictureSelected
                      pictures={productSelected.pictures}
                    />
                  </Col>
                  <Col
                    xs={6}
                    className="align-self-center justify-content-start"
                  >
                    <h1 className="h1">{productSelected.name}</h1>
                    <h3>R$ {productSelected.price}</h3>
                    <a href={productSelected.linkRedirectShop} rel="noreferrer" target="_blank">
                      <Button variant="primary">Link para loja</Button>
                    </a>
                  </Col>
                </>
              }
            </Row>
            <Row>
            <PriceCharts data={productSelected} />
            </Row>
            <br />
            <br />
            <TechnicalInformation
              productInformations={productSelected.descriptions}
            />
            <br />
            <br />
            <h1>Produtos relacionados</h1>
            {productRelatedPartOne.map((relatedProduct) => (
              <CardListProduct
                key={relatedProduct.id_product}
                productImg={relatedProduct.thumbImgLink}
                productName={relatedProduct.name}
                productId={relatedProduct.id_product}
                productPrice={relatedProduct.price}
                productLinkRedirec={relatedProduct.linkRedirectShop}
              />
            ))}
            <Accordion defaultActiveKey="1">
              {relatedProducts.map((relatedProduct) => (
                <Accordion.Collapse eventKey="0">
                  <CardListProduct
                    key={relatedProduct.id_product}
                    productImg={relatedProduct.thumbImgLink}
                    productId={relatedProduct.id_product}
                    productName={relatedProduct.name}
                    productPrice={relatedProduct.price}
                    productLinkRedirec={relatedProduct.linkRedirectShop}
                  />
                </Accordion.Collapse>
              ))}
              <Accordion.Toggle as="a" variant="link" eventKey="0">
                <Button variant="primary">Ver mais</Button>
              </Accordion.Toggle>
            </Accordion>
          </>
        )}
      </Container>
    </>
  );
};

export default ProdutoEscolhido;

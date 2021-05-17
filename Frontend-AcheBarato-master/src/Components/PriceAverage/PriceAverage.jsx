import { useState, useEffect } from "react";
import { getSearchedProducts } from "../../services/api.js";
import { useLocation } from "react-router";
import { Col, Container, Row } from "react-bootstrap";
import MenuSearchBar from "../MenuSearchBar";
import MenuAside from "../MenuAside/index";
import Footer from "../Footer/Footer.jsx";
import ProdutosPesquisados from "../ProdutosPesquisados/ProdutosPesquisados";
import ReactLoading from "react-loading";
import Pagination from "react-js-pagination";

const PriceAverage = () => {
  const [products, setProducts] = useState([]);
  const [isLoaded, setIsLoaded] = useState(false);
  const [minPrice, setMinPrice] = useState(0);
  const [maxPrice, setMaxPrice] = useState(99999);
  const [itemsPerPage, setItemsPerPage] = useState(0);
  const [totalPage, setTotalPage] = useState(0);
  const [totalItems, setTotalItems] = useState(0);
  const [pageNumber, setpageNumber] = useState(1);
  const [activePage, setActivePage] = useState(1);
  const [ordering, setOrdering] = useState(" ");

  function useQuery() {
    return new URLSearchParams(useLocation().search);
  }

  let urlParams = useQuery();

  const search = urlParams.get("search");

  async function loadProducts() {
    const response = await getSearchedProducts({
      search,
      minPrice,
      maxPrice,
      ordering,
      pageNumber,
    });
    setProducts(response.data.data);
    setItemsPerPage(response.data.limit);
    setTotalPage(response.data.totalPages);
    setTotalItems(response.data.total);

    setIsLoaded(true);
  }

  useEffect(() => {
    loadProducts();
    return setIsLoaded(false);
  }, [minPrice, maxPrice, ordering, pageNumber]);

  const handlerFilters = (minprice, maxprice, orderBy) => {
    setMinPrice(minprice);
    setOrdering(orderBy);
    setMaxPrice(maxprice);
  };

  const handlerPagination = (page) => {
    setpageNumber(page);
    setActivePage(page);
  };

  return (
    <div class="menu ">
      <MenuSearchBar />
      <Container fluid>
        <Row style={{ marginTop: "10px" }}>
          <aside class="animate-right">
            <MenuAside onChangeFilters={handlerFilters} />
          </aside>
          {isLoaded ? (
            <>
              <ProdutosPesquisados products={products} />
            </>
          ) : (
            <Col
              xl={10}
              xs={10}
              style={{
                marginLeft: "auto",
                marginRight: "auto",
                display: "block",
              }}
            >
              <ReactLoading
                type="balls"
                color="#ff6633"
                height={200}
                width={200}
              />
            </Col>
          )}
          <Col xl={12} xs={12} style={{ padding: "10px 50%" }}>
            <Pagination
              activePage={activePage}
              itemsCountPerPage={itemsPerPage}
              totalItemsCount={totalItems}
              pageRangeDisplayed={totalPage}
              onChange={handlerPagination}
              itemClass="page-item"
              linkClass="page-link"
            />
          </Col>
        </Row>
        <Footer />
      </Container>
    </div>
  );
};

export default PriceAverage;

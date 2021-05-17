import { InputGroup } from "react-bootstrap";
import { useState } from "react";

import {ButtonSearch, FormControlStyled} from "./SearchBar.js";
import { Link, useLocation, BrowserRouter } from "react-router-dom";

const SearchBar = () => {
  const [search, setSearch] = useState("");
  const location = useLocation();

  const handleSubmit = (event) => {
    event.preventDefault();
  };

  return (
    <>
    <form className="mr-auto ml-auto col-sm-9 " onSubmit={handleSubmit}>
    <InputGroup>
        <FormControlStyled
          placeholder="Search for a product"
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
        <BrowserRouter forceRefresh={true}>
        <Link style={{textDecoration:'none'}} to={search === "" ? `${location.pathname}` : `/ProdutosPesquisados?search=${search}`}>
        <ButtonSearch variant="secondary">Search</ButtonSearch> 
        </Link>
        </BrowserRouter>
      </InputGroup>
    </form>
      

    </>
  );
};

export default SearchBar;
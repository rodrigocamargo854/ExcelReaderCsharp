import { useState } from "react";
import { InputGroup } from "react-bootstrap";
import { ButtonStyled, FormControlStyled } from "./SearchBar.js";
import { Link } from "react-router-dom";


const SearchBar = () => {
  const [search, setSearch] = useState("");


  const handleSubmit = (event) => {
    event.preventDefault();
  };

  return (
    <form className="mr-auto ml-auto col-sm-9 " onSubmit={handleSubmit}>
      <InputGroup>
        <FormControlStyled
          placeholder="Search for a product"
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
        <Link to={search === "" ? "/" : `/ProdutosPesquisados?search=${search}`}>
          <ButtonStyled variant="outline-primary">Search</ButtonStyled>
        </Link>
      </InputGroup>
    </form>
  );
};

export default SearchBar;

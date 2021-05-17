import React from "react";
import CarouselProdutos from "../../Components/CarouselProducts/CarouselProducts";
import CarouselMainPage from '../../Components/CarouselMainPage/CarouselMainPage';
import SearchBar from "../../Components/SearchBar/SearchBar.jsx"
import Footer from '../../Components/Footer/Footer.jsx';
import { makeStyles } from '@material-ui/core/styles';
import Avatar from '@material-ui/core/Avatar';
import { deepOrange } from '@material-ui/core/colors';
import { FormatSize } from "@material-ui/icons";
import { Form, Col, Navbar, Nav, FormControl } from 'react-bootstrap';
import { Button } from "../../UI/Button/index";
import MenuSearchBar from "../../Components/MenuSearchBar/index";
import useAuth from "../../Context/hooks/useAuth"


const useStyles = makeStyles((theme) => ({
  root: {
    display: 'flex',
    '& > *': {
      margin: theme.spacing(1),
    },
  },
  orange: {
    color: theme.palette.getContrastText(deepOrange[500]),
    backgroundColor: deepOrange[500],
  },
}));

export default function MainPageProfile() {
  const {user} = useAuth();
  const classes = useStyles();

  return (
    <>
      <MenuSearchBar />
      <Form style={{ margin: 74 }}>
        <Avatar alt="Remy Sharp" src="/broken-image.jpg" className={classes.orange}>
          {user.name[0][0]}
        </Avatar>
        <Form.Row>
          <Form.Group as={Col} controlId="formGridEmail"   >
            <Form.Label>Email</Form.Label>
            <Form.Control type="email" placeholder={user.email} />
          </Form.Group>
          <Form.Group as={Col} controlId="formGridPassword" >
            <Form.Label>Password</Form.Label>
            <Form.Control type="password" placeholder= "**********" />
          </Form.Group>
        </Form.Row>
        <Form.Group controlId="formGridAddress1" style={{ width: 400 }}>
          <Form.Label>Celular</Form.Label>
          <Form.Control type="text" placeholder= {user.phoneNumber} />
        </Form.Group>

        <div class="buttonsubmit" style={{margin:80}}>
          <Button variant="primary" type="submit">
            Submit
          </Button>
        </div>

      </Form>
    </>
  );
}
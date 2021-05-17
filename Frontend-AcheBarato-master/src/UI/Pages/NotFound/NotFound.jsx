import React from 'react';
import "../../../Components/GlobalStyles.js";

import {
  Box,
  Container,
  Typography,
  makeStyles
} from '@material-ui/core';
import Page from './Page';

const useStyles = makeStyles((theme) => ({
  root: {
    background: 'linear-gradient(45deg, #e75004 100%, #FF8E53 50%)',
    backgroundColor: theme.palette.background.dark,
    height: '100%',
    paddingBottom: theme.spacing(3),
    paddingTop: theme.spacing(3),
    font:'Fira Sans'

  },
  image: {
    marginTop: 50,
    display: 'inline-block',
    maxWidth: '100%',
    width: 360
  }
}));

const NotFound = () => {
  const classes = useStyles();

  return (
    <Page
      className={classes.root}
      title="404"
    >
      <Box
        display="flex"
        flexDirection="column"
        height="100%"
        justifyContent="center"
      >
        <Container maxWidth="md">
          <Typography
            align="center"
            color="textPrimary"
            variant="h1"
          >
            404: The page you are looking for isn’t here
          </Typography>
          
          <Box textAlign="center">
            <img
              alt="Page Not Found"
              className={classes.image}
              src="/static/images/419.gif"
            />
          </Box>
        </Container>
      </Box>
    </Page>
  );
};

export default NotFound;

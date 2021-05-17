import React from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/core/styles';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import { TextField } from '@material-ui/core';
import { H3 } from '../AsideMenu/VerticalTabsStyles';
import CathegoryNavAside from '../CathegoryNavAside/CathegoryNavAside.jsx';
import usePriceFilter from '../../Context/hooks/usePriceFilter';
function TabPanel(props) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`vertical-tabpanel-${index}`}
      aria-labelledby={`vertical-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box p={3}>
          <Typography>{children}</Typography>
        </Box>
      )}
    </div>
  );
}

TabPanel.propTypes = {
  children: PropTypes.node,
  index: PropTypes.any.isRequired,
  value: PropTypes.any.isRequired,
};

function a11yProps(index) {
  return {
    id: `vertical-tab-${index}`,
    'aria-controls': `vertical-tabpanel-${index}`,
  };
}

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    backgroundColor: theme.palette.background.paper,
    display: 'flex',
    height: 530,
    width:174,
    marginTop:-40

  },
  tabs: {
    borderRight: `1px solid ${theme.palette.divider}`,
  },
}));

export default function VerticalTabs() {
  const classes = useStyles();
  const [value, setValue] = React.useState(0);
  const { price, handlerPrice } = usePriceFilter();
  
  const handleChange = (event, newValue) => {
    setValue(newValue);
  };

  console.log(value)

  return (
    <>
      <div className={classes.root}>
        <Tabs

          orientation="vertical"
          variant="scrollable"
          value={price}
          onChange={handleChange}
          aria-label="Vertical tabs example"
          className={classes.tabs}
        >
          <H3>Categorias</H3>
          
          <CathegoryNavAside/>
        </Tabs>
      
      </div>
     
    </>

  );
}


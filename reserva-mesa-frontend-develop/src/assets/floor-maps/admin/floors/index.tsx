import FloorBlumenau1 from "./floor.01-01";
import FloorBlumenau2 from "./floor.01-02";
import FloorBlumenau3 from "./floor.01-03";
import FloorBlumenau4 from "./floor.01-04";
import FloorBlumenau5 from "./floor.01-05";
import FloorBlumenau6 from "./floor.01-06";
import FloorBlumenau7 from "./floor.01-07";
import FloorBlumenau8 from "./floor.01-08";
import FloorBlumenau9 from "./floor.01-09";
import FloorBlumenau10 from "./floor.01-10";

interface Map {
  id?: number;
  code: string,
  map: any,
};

const maps: Map[] = [
  {
    code: "01-01",
    map: FloorBlumenau1
  },
  {
    code: "01-02",
    map: FloorBlumenau2
  },
  {
    code: "01-03",
    map: FloorBlumenau3
  },
  {
    code: "01-04",
    map: FloorBlumenau4
  },
  {
    code: "01-05",
    map: FloorBlumenau5
  },
  {
    code: "01-06",
    map: FloorBlumenau6
  },
  {
    code: "01-07",
    map: FloorBlumenau7
  },
  {
    code: "01-08",
    map: FloorBlumenau8
  },
  {
    code: "01-09",
    map: FloorBlumenau9
  },
  {
    code: "01-10",
    map: FloorBlumenau10
  },
];

export default maps;

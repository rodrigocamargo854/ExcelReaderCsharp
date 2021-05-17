import { useContext } from "react";
import { UserMapContext } from "~/contexts";

interface Map {
  id?: number;
  code: string,
  map: any,
};

interface MapProps {
  floors: Map[]
}

const Map: React.FC<MapProps> = (props) => {
  const { floorCode } = useContext(UserMapContext);
  let floors = props.floors;


  return (
    <div className="map">
      {floors.map((floor, index) => floor.code === floorCode && <floor.map key={index} />)}
    </div>
  );
}

export default Map;

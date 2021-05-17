import { useContext, useEffect, useState } from "react";
import { Select } from "antd";
import { SelectValue } from "antd/lib/select";

import { Unit } from "~/domain/models/unit";
import * as unitService from "~/services/http/backend/unit-service";

import { AdminMapContext } from "~/contexts";

const SelectUnit = () => {
  const { setUnitId } = useContext(AdminMapContext);

  const [units, setUnits] = useState<Unit[]>([]);

  useEffect(() => {
    (async () => {
      const units = (await unitService.getUnits()).data;
      setUnits(units.filter((unit) => unit.active));
    })();
  }, []);

  const handleChange = (event: SelectValue): void => {
    setUnitId(parseInt(event.toString()));
  };

  return (
    <div className="select-unit">
      <span>Unidade</span>
      <Select onChange={handleChange}>
        {units.map((unit) => (
          <Select.Option value={unit.id} key={unit.id}>
            {unit.name}
          </Select.Option>
        ))}
      </Select>
    </div>
  );
};

export default SelectUnit;

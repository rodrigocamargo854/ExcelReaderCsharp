import { Table } from "antd";
import { ColumnsType } from "antd/lib/table";
import React, { useContext, useEffect } from "react";
import { FloorsContext } from "~/contexts";
import { Floor } from "~/domain/models/floor";

const tableColumns: ColumnsType<Floor> = [
  {
    title: "Nome",
    dataIndex: "name",
  },
  {
    title: "Ativo",
    dataIndex: "active",
    render: (value: boolean) => (value ? "Sim" : "Não"),
  },
];

const FloorsTable = () => {
  const {
    floors,
    unitId,
    fetchFloors,
    setModalIsOpen,
    setSelectedFloor,
  } = useContext(FloorsContext);

  const onRow = (record: Floor) => ({
    onClick: () => {
      openModal(record);
    },
  });

  const openModal = (floor: Floor) => {
    if (unitId) {
      setSelectedFloor({ ...floor, unitId: unitId });
      setModalIsOpen(true);
    }
  }

  useEffect(() => {
    (async () => {
      await fetchFloors();
    })();
  }, [unitId]);

  return <Table dataSource={floors} columns={tableColumns} onRow={onRow} />;
};

export default FloorsTable;

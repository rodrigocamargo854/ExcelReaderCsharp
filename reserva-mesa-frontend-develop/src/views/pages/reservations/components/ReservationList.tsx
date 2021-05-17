import React, { useEffect, useContext, useState } from "react";
import { Table } from "antd";
import moment from "moment";

import {
  AuthenticatedUserReservationsContext
} from "~/contexts/AuthenticatedUserReservationsContext";
import { Reservation } from "~/domain/models/reservation";

interface ReservationListItem {
  key?: number;
  date: Date | string;
  workstationName: string;
}

const columns = [
  {
    title: 'Estação de Trabalho',
    dataIndex: 'workstationName'
  },
  {
    title: 'Data',
    dataIndex: 'date',
  }
];

const ReservationList = () => {
  const {
    initialDate,
    finalDate,
    actualPage,
    reservations,
    fetchReservations,
    setSelectedReservationsId,
    setActualPage
  } = useContext(AuthenticatedUserReservationsContext);

  useEffect(() => {
    (async () => {
      fetchReservations();
    })();
  }, [initialDate, finalDate, actualPage]);

  const rowSelection = {
    onChange: (selectedRowKeys: React.Key[], selectedRows: ReservationListItem[]) => {
      setSelectedReservationsId(selectedRows.map(selectedRow => selectedRow.key ? selectedRow.key : 0))
    }
  };

  const convertReservationsToReservationsListItems = (reservations: Reservation[]):
  ReservationListItem[] => {
    let listItems: ReservationListItem[] = [];
    reservations.forEach(({ id, date, workstationName }) => listItems.push({
      key: id,
      date: moment(date).format('DD/MM/YYYY'),
      workstationName
    }));
    return listItems;
  }

  return (
    <Table
      columns={columns}
      dataSource={convertReservationsToReservationsListItems(reservations)}
      rowSelection={{
        type: 'checkbox',
        ...rowSelection
      }}
      pagination={{
        onChange: () => setActualPage(actualPage + 1)
      }}
    />
  );
}

export default ReservationList;

import React, { useEffect, useState, useContext } from "react";

import { CheckIn } from "~/domain/models/check-in";
import { Table, Button, Input } from 'antd';
import { LeftOutlined, RightOutlined } from '@ant-design/icons'
import { getAllActiveReservationsFromToday } from '~/services/http/backend/reservation-service';
import { CheckInContext } from "~/contexts/CheckInContext";

const { Search } = Input;

const CheckInReservationList: React.FC = () => {
  const [reservations, setReservations] = useState<CheckIn[]>([])
  const [nameFilter, setNameFilter] = useState<string>("");
  const [page, setPage] = useState<number>(0);
  const [pageCount, setPageCount] = useState<number>(0);
  const { setModalIsOpen, setReservation, wasChanged, setWasChanged } = useContext(CheckInContext);

  useEffect(() => {
    (async () => {
      const { data, count } = (await getAllActiveReservationsFromToday(nameFilter, page));
      setPageCount(count / 14);
      setReservations(data);
      setWasChanged(false);
    })()
  }, [nameFilter, page, wasChanged]);

  const onSearch = (value: string) => {
    setNameFilter(value);
  }

  const nextPage = () => {
    if (page < pageCount - 1) {
      setPage(page + 1);
    }
  }

  const prevPage = () => {
    if (page > 0) {
      setPage(page - 1);
    }
  }

  const openModal = (row: CheckIn) => {
    setReservation(row);
    setModalIsOpen(true);
  }

  const columns = [
    {
      title: 'Nome',
      dataIndex: 'userName',
      key: 'userName'
    },
    {
      title: 'Estação de trabalho',
      dataIndex: 'workstationName',
      key: 'workstationName'
    },
    {
      title: 'Check In',
      dataIndex: '',
      key: 'x',
      render: (row: CheckIn) => {
        if (row.checkInStatus) {
          return <Button type="primary" disabled>Check In</Button>
        }
        return <Button type="primary" onClick={() => openModal(row)}>Check In</Button>
      }
    }
  ];

  return (
    <>
      <Search className="search" placeholder="Digite o nome do colaborador" onSearch={onSearch} />
      <Table dataSource={reservations} columns={columns} pagination={false} />
      <Button className="pag-btn" onClick={prevPage}><LeftOutlined /></Button>
      <Button className="pag-btn">{page + 1}</Button>
      <Button className="pag-btn" onClick={nextPage}><RightOutlined /></Button>
    </>
  );
}

export default CheckInReservationList;

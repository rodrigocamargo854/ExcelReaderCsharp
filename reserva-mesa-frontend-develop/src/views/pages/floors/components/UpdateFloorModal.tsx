import React, { useContext } from "react";
import { Modal, Form, Checkbox, Input } from "antd";

import { Floor, UpdateFloor } from "~/domain/models/floor";
import { updateFloor } from "~/services/http/backend/floor-service";
import { FloorsContext } from "~/contexts";
import { PopupStatus, showPopup } from "~/application/popup";
import { updateFloorSchema } from "~/domain/schemas/floor";
import type { ValidationError } from "yup";

const UpdateFloorModal = () => {
  const {
    selectedFloor,
    modalIsOpen,
    setSelectedFloor,
    setModalIsOpen,
    fetchFloors,
  } = useContext(FloorsContext);

  const onOk = async (): Promise<void> => {
    if (!selectedFloor) {
      return;
    }

    const requestModel: UpdateFloor = {
      active: selectedFloor.active,
      name: selectedFloor.name,
      unityId: selectedFloor.unitId
    };

    updateFloorSchema
      .validate(requestModel)
      .then(async () => {
        const response = await updateFloor(selectedFloor.id, requestModel);
        onUpdateFinish(response.statusCode);
      })
      .catch((err: ValidationError) => {
        showPopup({
          status: PopupStatus.Error,
          title: "Erro de validação",
          message: err.message
        });
      })
  };

  const onCancel = () => {
    setModalIsOpen(false);
    setSelectedFloor(undefined);
  };

  const onUpdateFinish = (responseStatusCode: number) => {
    setSelectedFloor(undefined);
    setModalIsOpen(false);

    showPopup({
      status:
        responseStatusCode === 200 ? PopupStatus.Success : PopupStatus.Error,
      title:
        responseStatusCode === 200
          ? "Andar atualizado."
          : "Houve um erro ao tentar atualizar andar.",
    });

    fetchFloors();
  };

  return (
    <Modal
      closable={false}
      okText="Alterar andar"
      cancelText="Descartar"
      onOk={onOk}
      onCancel={onCancel}
      visible={modalIsOpen}
    >
      <Form>
        <div className="floors-modal-text-input">
          <Form.Item label="Nome" name="name" />
          <Input
            value={selectedFloor?.name}
            onChange={(e) =>
              setSelectedFloor({
                ...selectedFloor,
                name: e.target.value,
              } as Floor)
            }
          />
        </div>
        <div className="floors-modal-checkbox">
          <Form.Item label="Ativo" name="active" />
          <Checkbox
            checked={selectedFloor?.active}
            onChange={(e) =>
              setSelectedFloor({
                ...selectedFloor,
                active: e.target.checked,
              } as Floor)
            }
          />
        </div>
      </Form>
    </Modal>
  );
};

export default UpdateFloorModal;

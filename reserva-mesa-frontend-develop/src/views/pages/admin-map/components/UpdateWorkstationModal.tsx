import React, { useContext } from "react";
import Modal from "antd/lib/modal/Modal";
import { UpdateWorkstationContext } from "~/contexts";
import { Workstation } from "~/domain/models/workstation";
import { Form, Checkbox } from "antd";
import { updateWorkstation } from "~/services/http/backend/workstation-service";
import { PopupStatus, showPopup } from "~/application/popup";

const UpdateWorkstationModal = () => {
  const {
    modalIsOpen,
    setModalIsOpen,
    setWorkstation,
    workstation,
  } = useContext(UpdateWorkstationContext);

  const onOk = async () => {
    const response = await updateWorkstation(workstation);

    const success = response.statusCode === 200;
    showPopup({
      status: success ? PopupStatus.Success : PopupStatus.Error,
      title: success
        ? "Estação de trabalho alterada."
        : "Erro ao tentar atualizar estação de trabalho",
    });

    setWorkstation({} as Workstation);
    setModalIsOpen(false);
  };

  const onCancel = async () => {
    setWorkstation({} as Workstation);
    setModalIsOpen(false);
  };

  return (
    <Modal
      title="Alterar Estação de Trabalho"
      closable={false}
      okText="Alterar"
      cancelText="Descartar"
      visible={modalIsOpen}
      onOk={onOk}
      onCancel={onCancel}
    >
      <div className="update-workstation-modal-form">
        <Form>
          <div className="update-workstation-modal-form-checkbox">
            <Form.Item label="Ativo" name="active" />
            <Checkbox
              checked={workstation.active}
              onChange={(e) =>
                setWorkstation({ ...workstation, active: e.target.checked })
              }
            />
          </div>
        </Form>
      </div>
    </Modal>
  );
};

export default UpdateWorkstationModal;

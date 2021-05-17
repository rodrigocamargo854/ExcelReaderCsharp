import sweetAlert from "sweetalert2";

import { PopupContent } from "./interfaces";
import { PopupStatus } from "./enums";

const showPopup = (content: PopupContent) => {
  sweetAlert.fire({
    title: content.title,
    html: content.message,
    icon: content.status,
    confirmButtonColor: '#FFC629'
  });
}

export { showPopup, PopupStatus };
export type { PopupContent };

import { PopupStatus } from "./enums";

export interface PopupContent {
  title: string;
  message?: string;
  status: PopupStatus
}

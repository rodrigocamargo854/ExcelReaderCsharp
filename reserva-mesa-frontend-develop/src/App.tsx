import React from "react";
import "antd/dist/antd.css";

import {
  ApplicationProvider,
  CreateReservationProvider,
  UserMapProvider,
  AdminMapProvider,
  UpdateWorkstationProvider,
  AuthenticatedUserReservationsProvider,
  FloorsProvider,
  CheckInProvider
} from "./contexts";

import Routes from "./routes";

function App() {
  return (
    <ApplicationProvider>
      <CreateReservationProvider>
        <UserMapProvider>
          <AdminMapProvider>
            <UpdateWorkstationProvider>
              <AuthenticatedUserReservationsProvider>
                <FloorsProvider>
                  <CheckInProvider>
                    <Routes />
                  </CheckInProvider>
                </FloorsProvider>
              </AuthenticatedUserReservationsProvider>
            </UpdateWorkstationProvider>
          </AdminMapProvider>
        </UserMapProvider>
      </CreateReservationProvider>
    </ApplicationProvider>
  );
}

export default App;

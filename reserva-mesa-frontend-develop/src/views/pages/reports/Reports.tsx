import React from "react";
import { Tabs } from "antd";

import { EmitReportTabContent } from "./components";
import { EmitReportTabs } from "./enums";

const { TabPane } = Tabs;

const Reports = () => {
  return (
    <Tabs defaultActiveKey={EmitReportTabs.ReservedWorkstations}>
      <TabPane tab="Estações reservadas" key={EmitReportTabs.ReservedWorkstations}>
        <EmitReportTabContent report={EmitReportTabs.ReservedWorkstations} />
      </TabPane>
      <TabPane tab="Estações utilizadas" key={EmitReportTabs.ConfirmedWorkstations}>
        <EmitReportTabContent report={EmitReportTabs.ConfirmedWorkstations} />
      </TabPane>
    </Tabs>
  );
};

export default Reports;

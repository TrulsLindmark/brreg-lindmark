import {
  ArcElement,
  ChartData,
  Chart as ChartJS,
  Legend,
  Tooltip,
} from "chart.js";
import { Doughnut } from "react-chartjs-2";

ChartJS.register(ArcElement, Tooltip, Legend);

ChartJS.defaults.plugins.legend.position = "right";
ChartJS.defaults.plugins.legend.align = "center";
ChartJS.defaults.plugins.legend.labels.boxWidth = 12;
ChartJS.defaults.plugins.legend.labels.padding = 10;
ChartJS.defaults.plugins.legend.labels.usePointStyle = true;
ChartJS.defaults.plugins.legend.labels.boxWidth = 4;
ChartJS.defaults.plugins.legend.labels.boxHeight = 6;
ChartJS.overrides.doughnut.cutout = "80%";

ChartJS.overrides.doughnut.plugins.legend.onHover = (
  _: any,
  legendItem: any,
  legend: any
) => {
  // Show tooltip on hover
  // See https://stackoverflow.com/a/72810877
  const chart = legend.chart;
  const activeElement = {
    datasetIndex: 0,
    index: legendItem.index,
  };
  chart.tooltip.setActiveElements([activeElement]);
  chart.update();
};

interface IProps {
  data: ChartData<"doughnut", number[], string>;
}

export function DoughtnutChart(props: IProps) {
  const { data } = props;

  return (
    <div>
      <Doughnut data={data} />
    </div>
  );
}

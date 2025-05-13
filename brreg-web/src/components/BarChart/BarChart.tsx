import {
  BarController,
  BarElement,
  CategoryScale,
  ChartData,
  Chart as ChartJS,
  Legend,
  LinearScale,
  LineController,
  LineElement,
  PointElement,
  Tooltip,
} from "chart.js";
import { Chart } from "react-chartjs-2";

ChartJS.register(
  LinearScale,
  CategoryScale,
  BarElement,
  PointElement,
  LineElement,
  Legend,
  Tooltip,
  LineController,
  BarController
);

const options = {
  plugins: {
    legend: {
      display: false,
      position: "top" as const,
    },
  },
  responsive: true,
};

interface IProps {
  data: ChartData<"bar", number[], string>;
}

export function BarChart(props: IProps) {
  const { data } = props;
  return <Chart type="bar" data={data} options={options} />;
}

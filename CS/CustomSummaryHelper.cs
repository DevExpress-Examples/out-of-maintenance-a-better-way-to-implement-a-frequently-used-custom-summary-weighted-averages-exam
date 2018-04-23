using System;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace WeightedAverages {
	public class CustomSummaryHelper {

		public static object GetWeightedAverage(GridView view, string weightField, string valueField) {
			const decimal undefined = 0;
			if(view == null) return undefined;
			GridColumn weightCol = view.Columns[weightField];
			GridColumn valueCol = view.Columns[valueField];
			if(weightCol == null || valueCol == null) return undefined;
			
			try {
				decimal totalWeight = 0, totalValue = 0;
				for(int i = 0; i < view.DataRowCount; i++) {
					if(view.IsNewItemRow(i)) continue;
					object temp;
					decimal weight, val;
					temp = view.GetRowCellValue(i, weightCol);
					weight = (temp == DBNull.Value || temp == null) ? 0 : Convert.ToDecimal(temp);
					temp = view.GetRowCellValue(i, valueCol);
					val = (temp == DBNull.Value || temp == null) ? 0 : Convert.ToDecimal(temp);

					totalWeight += weight;
					totalValue += weight * val;
				}
				if(totalWeight == 0) return undefined;
				return totalValue / totalWeight;
			}
			catch {
				return undefined;
			}
		}		
	}
}

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports DevExpress.Data
Imports DevExpress.XtraGrid.Views.Grid

Namespace WeightedAverages
    ''' <summary>
    ''' Summary description for Form1.
    ''' </summary>
    Public Class Form1
        Inherits System.Windows.Forms.Form

        Private gridControl1 As DevExpress.XtraGrid.GridControl
        Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
        Private colOrderID As DevExpress.XtraGrid.Columns.GridColumn
        Private colProduct As DevExpress.XtraGrid.Columns.GridColumn
        Private colQuantity As DevExpress.XtraGrid.Columns.GridColumn
        Private colUnitPrice As DevExpress.XtraGrid.Columns.GridColumn
        Private dataSet2 As DataSet
        Private components As IContainer

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
            Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
            Me.colOrderID = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colProduct = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colUnitPrice = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.dataSet2 = New System.Data.DataSet()
            DirectCast(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.dataSet2, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' gridControl1
            ' 
            Me.gridControl1.DataMember = "OrderDetails"
            Me.gridControl1.DataSource = Me.dataSet2
            Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.gridControl1.EmbeddedNavigator.Name = ""
            Me.gridControl1.Location = New System.Drawing.Point(0, 0)
            Me.gridControl1.MainView = Me.gridView1
            Me.gridControl1.Name = "gridControl1"
            Me.gridControl1.Size = New System.Drawing.Size(459, 316)
            Me.gridControl1.TabIndex = 0
            Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1})
            ' 
            ' gridView1
            ' 
            Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() { Me.colOrderID, Me.colProduct, Me.colQuantity, Me.colUnitPrice})
            Me.gridView1.GridControl = Me.gridControl1
            Me.gridView1.Name = "gridView1"
            Me.gridView1.OptionsView.ShowFooter = True
            Me.gridView1.OptionsView.ShowGroupPanel = False
            ' 
            ' colOrderID
            ' 
            Me.colOrderID.Caption = "OrderID"
            Me.colOrderID.FieldName = "OrderID"
            Me.colOrderID.Name = "colOrderID"
            Me.colOrderID.Visible = True
            Me.colOrderID.VisibleIndex = 0
            Me.colOrderID.Width = 70
            ' 
            ' colProduct
            ' 
            Me.colProduct.Caption = "Product"
            Me.colProduct.FieldName = "Product"
            Me.colProduct.Name = "colProduct"
            Me.colProduct.Visible = True
            Me.colProduct.VisibleIndex = 1
            Me.colProduct.Width = 164
            ' 
            ' colQuantity
            ' 
            Me.colQuantity.Caption = "Quantity"
            Me.colQuantity.FieldName = "Quantity"
            Me.colQuantity.Name = "colQuantity"
            Me.colQuantity.Visible = True
            Me.colQuantity.VisibleIndex = 2
            Me.colQuantity.Width = 74
            ' 
            ' colUnitPrice
            ' 
            Me.colUnitPrice.Caption = "UnitPrice"
            Me.colUnitPrice.DisplayFormat.FormatString = "c2"
            Me.colUnitPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            Me.colUnitPrice.FieldName = "UnitPrice"
            Me.colUnitPrice.Name = "colUnitPrice"
            Me.colUnitPrice.SummaryItem.DisplayFormat = "W Avg: {0:c2}"
            Me.colUnitPrice.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
            Me.colUnitPrice.Visible = True
            Me.colUnitPrice.VisibleIndex = 3
            Me.colUnitPrice.Width = 137
            ' 
            ' dataSet2
            ' 
            Me.dataSet2.DataSetName = "NewDataSet"
            ' 
            ' Form1
            ' 
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(459, 316)
            Me.Controls.Add(Me.gridControl1)
            Me.Name = "Form1"
            Me.Text = "Form1"
            DirectCast(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.dataSet2, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        #End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread> _
        Shared Sub Main()
            Application.Run(New Form1())
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            dataSet2.ReadXml("..\..\Data2.xml")
        End Sub

        Private Sub gridView1_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles gridView1.CustomSummaryCalculate
            If e.IsTotalSummary AndAlso e.SummaryProcess = CustomSummaryProcess.Finalize Then
                Dim view As GridView = TryCast(sender, GridView)
                If e.Item = view.Columns("UnitPrice").SummaryItem Then
                    e.TotalValue = CustomSummaryHelper.GetWeightedAverage(view, "Quantity", "UnitPrice")
                End If
            End If
        End Sub
    End Class
End Namespace

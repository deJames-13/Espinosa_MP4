Public Class Form1
    Dim fGr, fTxRt, fNet, fBnft, fPayRt, fPlanRt, fFedTax, fAccIns, fOt, fHrs As Decimal

    Private Sub btnCalc_Click(sender As Object, e As EventArgs) Handles btnCalc.Click
        ' VALIDATING TEXTBOX
        If Not IsNumeric(txtHrs.Text) OrElse Not IsNumeric(txtPayr.Text) Then
            MsgBox("Invalid numeric input!", MsgBoxStyle.Exclamation, "Invalid Input")
            Return
        End If
        ' INPUTS to NUMERIC
        fHrs = Decimal.Parse(txtHrs.Text, Globalization.NumberStyles.Currency)
        fPayRt = Decimal.Parse(txtPayr.Text, Globalization.NumberStyles.Currency)

        ' PROCESS
        If Not (fHrs > 0 AndAlso fHrs <= 60) Then
            MsgBox("Hours work not in range! (Working ours 0-60)", MsgBoxStyle.Exclamation, "Invalid Input")
            Return
        End If
        fOt = 0
        If fHrs > 40 Then
            fOt = (fHrs - 40) * (0.5 * fPayRt)
        End If

        fGr = fPayRt * fHrs + fOt

        If fGr > 0 AndAlso fGr <= 985 Then
            fTxRt = 0.08
        ElseIf fGr > 985.01 AndAlso fGr <= 2450 Then
            fTxRt = 0.18
        ElseIf fGr > 2450.01 Then
            fTxRt = 0.28
        End If

        fFedTax = fGr * fTxRt

        If rbtDefault.Checked Then
            fPlanRt = 0
        ElseIf rbtStandard.Checked Then
            fPlanRt = 0.05
        ElseIf rbtPlan.Checked Then
            fPlanRt = 0.08
        End If

        fAccIns = 0
        If chkMed.Checked Then
            fAccIns += 37.75
        End If
        If chkLife.Checked Then
            'fAccIns += 18.75
            fAccIns += 18.35
        End If
        If chkDent.Checked Then
            fAccIns += 4.0
        End If

        fBnft = (fGr * fPlanRt) + fAccIns
        fNet = fGr - (fBnft + fFedTax)

        txtPayr.Text = Decimal.Parse(txtPayr.Text, Globalization.NumberStyles.Currency).ToString("c2")
        txtNet.Text = fNet.ToString("c2")
        txtBenefits.Text = fBnft.ToString("c2")
        txtTax.Text = fFedTax.ToString("c2")
        txtGross.Text = fGr.ToString("c2")

    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim m As Integer = MessageBox.Show("Are you sure you want to clear the form? ", "Clear Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If m = 7 Then
            Return
        End If
        txtName.Clear()
        mtxtEmpId.Clear()
        txtDept.Clear()
        txtHrs.Clear()
        txtPayr.Clear()
        txtGross.Clear()
        txtTax.Clear()
        txtBenefits.Clear()
        txtNet.Clear()
        rbtDefault.Checked = True
        rbtPlan.Checked = False
        rbtStandard.Checked = False
        chkDent.Checked = False
        chkMed.Checked = False
        chkLife.Checked = False
    End Sub


End Class

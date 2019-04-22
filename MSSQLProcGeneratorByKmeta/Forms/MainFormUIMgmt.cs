using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSSQLProcGeneratorByKmeta.Forms
{
    public partial class MainForm
    {
        /// <summary>
        /// Exit the application 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Sets the bool outputall if in the script generation it will only return out with new PK or all the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                outputAll = true;
            }
        }
        /// <summary>
        /// Sets the bool outputall if in the script generation it will only return out with new PK or all the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void returnID_CheckedChanged(object sender, EventArgs e)
        {
            if (returnID.Checked)
            {
                outputAll = false;
            }
        }
        /// <summary>
        /// Button Gen Procs click this generates the Stored Procedures
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btGenProcs_Click(object sender, EventArgs e)
        {
            if (comboDbName.SelectedIndex != -1)
            {
                GetDBData(configurationFolderPath + customColumnMappingFileName);
                ParseProcs(dbData, outputAll, currentAppOperaitons
                          , tbDestDir.Text, tbCreatorInitials.Text, customColumns);
            }
            else
            {
                MessageBox.Show("Please select Database!!!");
            }
        }
        /// <summary>
        /// Set current DB nabe based on combobox selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboDbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboDbName.SelectedIndex == -1)
            {
                currentDB = string.Empty;
                this.btSQLFormat.Enabled = false;
            }
            else
            {
                currentDB = comboDbName.SelectedItem.ToString();
                this.btSQLFormat.Enabled = true;
            }
        }
        /// <summary>
        /// Open file Dialog for choosing the Destination Dir where the scripts
        /// will be generated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDestDir_Click(object sender, EventArgs e)
        {
            if (procsDestDir.ShowDialog() == DialogResult.OK)
            {
                tbDestDir.Text = procsDestDir.SelectedPath;
            }
        }
        /// <summary>
        /// Sets the bool value if we want Custom Columns Mapping
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCustomColumnsMapping_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCustomColumnsMapping.Checked == true)
            {
                customColumns = true;
            }
            else
            {
                customColumns = false;
            }
        }
        /// <summary>
        /// Opens new From with SQL Script Formater and Script Parser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSQLFormat_Click(object sender, EventArgs e)
        {
            SQLFormatForm myform = new SQLFormatForm((cbFormatSQL.Checked == true ? true : false), currentDB);
            myform.Show();
        }
    }
}

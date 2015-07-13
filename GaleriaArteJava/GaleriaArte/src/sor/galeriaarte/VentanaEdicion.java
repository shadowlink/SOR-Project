/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sor.galeriaarte;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.xml.datatype.DatatypeConfigurationException;
import javax.xml.datatype.DatatypeFactory;
import javax.xml.datatype.XMLGregorianCalendar;
import org.joda.time.DateTime;
import org.tempuri.Ventas;
import sor.galeriaarte.clases.Juddi;

/**
 *
 * @author Victo
 */
public class VentanaEdicion extends javax.swing.JFrame {

    int ventaId;
    /**
     * Creates new form VentanaEdicion
     * @param id
     */
    public VentanaEdicion() {
        initComponents();
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jLabel9 = new javax.swing.JLabel();
        tbDia = new javax.swing.JTextField();
        tbMes = new javax.swing.JTextField();
        tbAnyo = new javax.swing.JTextField();
        tbMinuto = new javax.swing.JTextField();
        tbHora = new javax.swing.JTextField();
        jLabel12 = new javax.swing.JLabel();
        btGuardar = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        setTitle("Editar");
        setResizable(false);
        setType(java.awt.Window.Type.POPUP);

        jLabel9.setFont(new java.awt.Font("Segoe UI", 0, 12)); // NOI18N
        jLabel9.setText("Tiempo Max.");

        tbDia.setHorizontalAlignment(javax.swing.JTextField.CENTER);
        tbDia.setText("DD");
        tbDia.setPreferredSize(new java.awt.Dimension(120, 23));

        tbMes.setHorizontalAlignment(javax.swing.JTextField.CENTER);
        tbMes.setText("MM");
        tbMes.setPreferredSize(new java.awt.Dimension(120, 23));
        tbMes.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                tbMesActionPerformed(evt);
            }
        });

        tbAnyo.setHorizontalAlignment(javax.swing.JTextField.CENTER);
        tbAnyo.setText("AAAA");
        tbAnyo.setPreferredSize(new java.awt.Dimension(120, 23));

        tbMinuto.setHorizontalAlignment(javax.swing.JTextField.CENTER);
        tbMinuto.setText("MM");
        tbMinuto.setPreferredSize(new java.awt.Dimension(120, 23));

        tbHora.setHorizontalAlignment(javax.swing.JTextField.CENTER);
        tbHora.setText("HH");
        tbHora.setPreferredSize(new java.awt.Dimension(120, 23));

        jLabel12.setText(":");

        btGuardar.setText("Guardar");
        btGuardar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btGuardarActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGap(34, 34, 34)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(btGuardar)
                        .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(jLabel9)
                        .addGap(18, 18, 18)
                        .addComponent(tbDia, javax.swing.GroupLayout.PREFERRED_SIZE, 29, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(tbMes, javax.swing.GroupLayout.PREFERRED_SIZE, 29, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tbAnyo, javax.swing.GroupLayout.DEFAULT_SIZE, 47, Short.MAX_VALUE)
                        .addGap(33, 33, 33)
                        .addComponent(tbHora, javax.swing.GroupLayout.PREFERRED_SIZE, 41, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(jLabel12)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tbMinuto, javax.swing.GroupLayout.PREFERRED_SIZE, 41, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(34, 34, 34))))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGap(21, 21, 21)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel9)
                    .addComponent(tbDia, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(tbMes, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(tbAnyo, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(tbHora, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(tbMinuto, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel12))
                .addGap(18, 18, 18)
                .addComponent(btGuardar)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void tbMesActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_tbMesActionPerformed
        // TODO add your handling code here:
    }//GEN-LAST:event_tbMesActionPerformed

    public void setId(int id){
        ventaId = id;
    }
    
    private void btGuardarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btGuardarActionPerformed
        
        if (!"".equals(tbHora.getText()) && !"".equals(tbMinuto.getText()))
        {
            if (Integer.parseInt(tbHora.getText()) < 24 && Integer.parseInt(tbHora.getText()) >= 0)
            {
                if (Integer.parseInt(tbMinuto.getText()) < 60 && Integer.parseInt(tbMinuto.getText()) >= 0)
                {
                    if(Integer.parseInt(tbDia.getText()) >0 && Integer.parseInt(tbDia.getText())<=31){
                        if(Integer.parseInt(tbMes.getText()) >0 && Integer.parseInt(tbMes.getText())<=12){
                            DateTime date = new DateTime(Integer.parseInt(tbAnyo.getText()), Integer.parseInt(tbMes.getText()), Integer.parseInt(tbDia.getText()), Integer.parseInt(tbHora.getText()), Integer.parseInt(tbMinuto.getText()), 0);
                            
                            if (date.compareTo(DateTime.now())>0)
                            {
                                try {
                                    URL u = new URL(new Juddi().getServiceUrl("Ventas"));
                                    Ventas serv = new Ventas(u);
                                    Gson gson = new GsonBuilder().create();
                                    XMLGregorianCalendar xgc = DatatypeFactory.newInstance().newXMLGregorianCalendar(date.toGregorianCalendar());
                                    serv.getVentasSoap().editarVenta(ventaId, xgc);
                                    this.setVisible(false);
                                    this.dispose();
                                } catch (MalformedURLException ex) {
                                    Logger.getLogger(VentanaEdicion.class.getName()).log(Level.SEVERE, null, ex);
                                } catch (DatatypeConfigurationException ex) {
                                    Logger.getLogger(VentanaEdicion.class.getName()).log(Level.SEVERE, null, ex);
                                }
                            }
                        }
                    }
                }
            }
        }
    }//GEN-LAST:event_btGuardarActionPerformed

    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(VentanaEdicion.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(VentanaEdicion.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(VentanaEdicion.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(VentanaEdicion.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new VentanaEdicion().setVisible(true);
            }
        });
    }

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton btGuardar;
    private javax.swing.JLabel jLabel12;
    private javax.swing.JLabel jLabel9;
    private javax.swing.JTextField tbAnyo;
    private javax.swing.JTextField tbDia;
    private javax.swing.JTextField tbHora;
    private javax.swing.JTextField tbMes;
    private javax.swing.JTextField tbMinuto;
    // End of variables declaration//GEN-END:variables
}

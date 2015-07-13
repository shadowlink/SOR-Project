/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sor.galeriaarte;

import sor.galeriaarte.clases.Puja;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.reflect.TypeToken;
import sor.galeriaarte.clases.Auxiliar;
import java.awt.CardLayout;
import java.awt.Color;
import java.awt.Rectangle;
import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;
import java.net.MalformedURLException;
import java.net.URL;
import java.security.NoSuchAlgorithmException;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
import java.util.List;
import java.util.TimeZone;
import java.util.UUID;
import java.util.logging.Level;
import java.util.logging.Logger;
import static javafx.scene.input.KeyCode.T;
import javax.jms.Connection;
import javax.jms.ConnectionFactory;
import javax.jms.DeliveryMode;
import javax.jms.Destination;
import javax.jms.ExceptionListener;
import javax.jms.JMSException;
import javax.jms.Message;
import javax.jms.MessageConsumer;
import javax.jms.MessageListener;
import javax.jms.MessageProducer;
import javax.jms.Session;
import javax.jms.TextMessage;
import javax.jms.Topic;
import javax.swing.event.TableModelEvent;
import javax.swing.event.TableModelListener;
import javax.swing.table.DefaultTableModel;
import org.apache.activemq.ActiveMQConnectionFactory;
import org.joda.time.DateTime;
import org.joda.time.DateTimeZone;
import org.joda.time.LocalDateTime;
import org.joda.time.format.DateTimeFormat;
import org.joda.time.format.DateTimeFormatter;
import org.tempuri.Historico;
import org.tempuri.Usuarios;
import org.tempuri.Ventas;
import sor.galeriaarte.clases.Juddi;
import sor.galeriaarte.clases.Usuario;
import sor.galeriaarte.clases.Venta;

/**
 *
 * @author VíctorManuel
 */
public final class MainWindow extends javax.swing.JFrame implements MessageListener{
    
    Usuario user = new Usuario();
    /**
     * Creates new form MainWindow
     * @param u
     */
    public MainWindow(Usuario u) throws JMSException {
        user = u;
        initComponents();
        
        lblName.setText(u.Nombre);
        
        //Rellenamos la lista de pujas
        actualizarListaPujas();
        //Rellenamos la lista de ventas del cliente actual
        actualizarListaVentas();
        //Rellenamos el Historial
        actualizarListaHistorial();
        //Rellenar perfil
        txtNombre.setText(user.Nombre);
        txtMail.setText(user.Mail);
        
        
        //Listener para la tabla pujas
        lstPujas.getModel().addTableModelListener(new TableModelListener() {
            @Override
            public void tableChanged(TableModelEvent e) {
                if(e.getType() == 0){
                     int row = lstPujas.getEditingRow();
                     int column = lstPujas.getEditingColumn();

                     String resul = lstPujas.getValueAt(row, column).toString();

                     System.out.println(resul);
                }
            }
        });
        
        //TOPIC Obras
        MessageConsumer messageConsumer;
        Connection connection;
        Session session;
        ConnectionFactory connectionFactory = new ActiveMQConnectionFactory("tcp://192.168.2.16:61616");
        connection = connectionFactory.createConnection();
        connection.setClientID(UUID.randomUUID().toString());
        session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
        Topic topic = session.createTopic("Obras");
        messageConsumer = session.createDurableSubscriber(topic, UUID.randomUUID().toString());
        messageConsumer.setMessageListener(this);
        connection.start();
        
        //TOPIC Pujas
        MessageConsumer messageConsumer2;
        Connection connection2;
        Session session2;
        ConnectionFactory connectionFactory2 = new ActiveMQConnectionFactory("tcp://192.168.2.16:61616");
        connection2 = connectionFactory2.createConnection();
        connection2.setClientID(UUID.randomUUID().toString());
        session2 = connection2.createSession(false, Session.AUTO_ACKNOWLEDGE);
        Topic topic2 = session2.createTopic("Pujas");
        messageConsumer2 = session2.createDurableSubscriber(topic2, UUID.randomUUID().toString());
        messageConsumer2.setMessageListener(this);
        connection2.start();
        
        //TOPIC Ventas
        MessageConsumer messageConsumer3;
        Connection connection3;
        Session session3;
        ConnectionFactory connectionFactory3 = new ActiveMQConnectionFactory("tcp://192.168.2.16:61616");
        connection3 = connectionFactory3.createConnection();
        connection3.setClientID(UUID.randomUUID().toString());
        session3 = connection3.createSession(false, Session.AUTO_ACKNOWLEDGE);
        Topic topic3 = session3.createTopic("Ventas");
        messageConsumer3 = session3.createDurableSubscriber(topic3, UUID.randomUUID().toString());
        messageConsumer3.setMessageListener(this);
        connection3.start();
        
    }
    
    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        stkTest = new javax.swing.JPanel();
        pPujas = new javax.swing.JPanel();
        jScrollPane3 = new javax.swing.JScrollPane();
        lstPujas = new javax.swing.JTable();
        jLabel14 = new javax.swing.JLabel();
        btnPuja = new javax.swing.JButton();
        txtPuja = new javax.swing.JTextField();
        jLabel15 = new javax.swing.JLabel();
        jLabel11 = new javax.swing.JLabel();
        pHistorial = new javax.swing.JPanel();
        jLabel3 = new javax.swing.JLabel();
        jScrollPane4 = new javax.swing.JScrollPane();
        lstHistorico = new javax.swing.JTable();
        pVentas = new javax.swing.JPanel();
        jSeparator1 = new javax.swing.JSeparator();
        jLabel4 = new javax.swing.JLabel();
        jLabel5 = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        jLabel6 = new javax.swing.JLabel();
        jLabel7 = new javax.swing.JLabel();
        jLabel8 = new javax.swing.JLabel();
        jLabel9 = new javax.swing.JLabel();
        jLabel10 = new javax.swing.JLabel();
        tbTipo = new javax.swing.JTextField();
        tbAutor = new javax.swing.JTextField();
        tbAño = new javax.swing.JTextField();
        tbEstado = new javax.swing.JTextField();
        tbDia = new javax.swing.JTextField();
        tbPrecio = new javax.swing.JTextField();
        bAñadir = new javax.swing.JButton();
        jScrollPane1 = new javax.swing.JScrollPane();
        lstVentas = new javax.swing.JTable();
        tbMes = new javax.swing.JTextField();
        tbAnyo = new javax.swing.JTextField();
        tbMinuto = new javax.swing.JTextField();
        tbHora = new javax.swing.JTextField();
        jLabel12 = new javax.swing.JLabel();
        jLabel13 = new javax.swing.JLabel();
        btnVender = new javax.swing.JButton();
        btnCancelar = new javax.swing.JButton();
        btEditar = new javax.swing.JButton();
        jLabel23 = new javax.swing.JLabel();
        comboNegociado = new javax.swing.JComboBox();
        lbMessage = new javax.swing.JLabel();
        pPerfil = new javax.swing.JPanel();
        jLabel16 = new javax.swing.JLabel();
        jLabel17 = new javax.swing.JLabel();
        jLabel18 = new javax.swing.JLabel();
        jLabel19 = new javax.swing.JLabel();
        jLabel20 = new javax.swing.JLabel();
        jLabel21 = new javax.swing.JLabel();
        jLabel22 = new javax.swing.JLabel();
        txtNombre = new javax.swing.JTextField();
        txtMail = new javax.swing.JTextField();
        btConfirmar = new javax.swing.JButton();
        lbPMessage = new javax.swing.JLabel();
        jPasswordField1 = new javax.swing.JPasswordField();
        jPasswordField2 = new javax.swing.JPasswordField();
        btnVentas = new javax.swing.JButton();
        jButton2 = new javax.swing.JButton();
        jbHistorial = new javax.swing.JButton();
        jLabel1 = new javax.swing.JLabel();
        jScrollPane2 = new javax.swing.JScrollPane();
        txtEvent = new javax.swing.JTextArea();
        jPerfil = new javax.swing.JButton();
        lblName = new javax.swing.JLabel();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setBackground(new java.awt.Color(255, 255, 255));
        setResizable(false);

        stkTest.setBackground(new java.awt.Color(255, 255, 255));
        stkTest.setPreferredSize(new java.awt.Dimension(899, 430));
        stkTest.setLayout(new java.awt.CardLayout());

        lstPujas.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {

            },
            new String [] {
                "id", "Tipo", "Autor", "Estado", "Fecha Fin", "Puja Max."
            }
        ) {
            Class[] types = new Class [] {
                java.lang.Integer.class, java.lang.String.class, java.lang.String.class, java.lang.String.class, java.lang.String.class, java.lang.Integer.class
            };
            boolean[] canEdit = new boolean [] {
                false, false, false, false, false, false
            };

            public Class getColumnClass(int columnIndex) {
                return types [columnIndex];
            }

            public boolean isCellEditable(int rowIndex, int columnIndex) {
                return canEdit [columnIndex];
            }
        });
        jScrollPane3.setViewportView(lstPujas);

        jLabel14.setFont(new java.awt.Font("Segoe UI", 1, 20)); // NOI18N
        jLabel14.setText("Listado de obras");

        btnPuja.setText("Pujar");
        btnPuja.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnPujaActionPerformed(evt);
            }
        });

        txtPuja.setText("0");

        jLabel15.setText("€");

        jLabel11.setFont(new java.awt.Font("Tahoma", 1, 14)); // NOI18N
        jLabel11.setText("Eventos");

        javax.swing.GroupLayout pPujasLayout = new javax.swing.GroupLayout(pPujas);
        pPujas.setLayout(pPujasLayout);
        pPujasLayout.setHorizontalGroup(
            pPujasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(pPujasLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(pPujasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(pPujasLayout.createSequentialGroup()
                        .addGroup(pPujasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                            .addComponent(jLabel14)
                            .addGroup(pPujasLayout.createSequentialGroup()
                                .addComponent(btnPuja)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(txtPuja)))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(jLabel15)
                        .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                    .addComponent(jScrollPane3, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.DEFAULT_SIZE, 1010, Short.MAX_VALUE)))
            .addGroup(pPujasLayout.createSequentialGroup()
                .addComponent(jLabel11)
                .addGap(0, 0, Short.MAX_VALUE))
        );
        pPujasLayout.setVerticalGroup(
            pPujasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, pPujasLayout.createSequentialGroup()
                .addGap(30, 30, 30)
                .addComponent(jLabel14)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(jScrollPane3, javax.swing.GroupLayout.PREFERRED_SIZE, 320, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(pPujasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(btnPuja)
                    .addComponent(txtPuja, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel15))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jLabel11)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        stkTest.add(pPujas, "cPujas");

        jLabel3.setFont(new java.awt.Font("Segoe UI", 1, 20)); // NOI18N
        jLabel3.setText("Histórico de Ventas");

        lstHistorico.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {

            },
            new String [] {
                "id", "Tipo", "Estado", "Precio Venta"
            }
        ) {
            Class[] types = new Class [] {
                java.lang.Integer.class, java.lang.String.class, java.lang.String.class, java.lang.Integer.class
            };
            boolean[] canEdit = new boolean [] {
                false, false, true, false
            };

            public Class getColumnClass(int columnIndex) {
                return types [columnIndex];
            }

            public boolean isCellEditable(int rowIndex, int columnIndex) {
                return canEdit [columnIndex];
            }
        });
        lstHistorico.getTableHeader().setReorderingAllowed(false);
        jScrollPane4.setViewportView(lstHistorico);

        javax.swing.GroupLayout pHistorialLayout = new javax.swing.GroupLayout(pHistorial);
        pHistorial.setLayout(pHistorialLayout);
        pHistorialLayout.setHorizontalGroup(
            pHistorialLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(pHistorialLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(pHistorialLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jScrollPane4, javax.swing.GroupLayout.PREFERRED_SIZE, 948, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel3))
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        pHistorialLayout.setVerticalGroup(
            pHistorialLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(pHistorialLayout.createSequentialGroup()
                .addGap(40, 40, 40)
                .addComponent(jLabel3)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(jScrollPane4)
                .addContainerGap())
        );

        stkTest.add(pHistorial, "cHistorial");

        pVentas.setBackground(new java.awt.Color(255, 255, 255));

        jSeparator1.setOrientation(javax.swing.SwingConstants.VERTICAL);

        jLabel4.setFont(new java.awt.Font("Segoe UI", 1, 20)); // NOI18N
        jLabel4.setText("Nueva Venta");

        jLabel5.setFont(new java.awt.Font("Segoe UI", 1, 20)); // NOI18N
        jLabel5.setText("Ventas en curso");

        jLabel2.setFont(new java.awt.Font("Segoe UI", 0, 12)); // NOI18N
        jLabel2.setText("Tipo");

        jLabel6.setFont(new java.awt.Font("Segoe UI", 0, 12)); // NOI18N
        jLabel6.setText("Autor");

        jLabel7.setFont(new java.awt.Font("Segoe UI", 0, 12)); // NOI18N
        jLabel7.setText("Año");

        jLabel8.setFont(new java.awt.Font("Segoe UI", 0, 12)); // NOI18N
        jLabel8.setText("Estado");

        jLabel9.setFont(new java.awt.Font("Segoe UI", 0, 12)); // NOI18N
        jLabel9.setText("Tiempo Max.");

        jLabel10.setFont(new java.awt.Font("Segoe UI", 0, 12)); // NOI18N
        jLabel10.setText("Precio");

        tbTipo.setPreferredSize(new java.awt.Dimension(120, 23));

        tbAutor.setPreferredSize(new java.awt.Dimension(120, 23));

        tbAño.setPreferredSize(new java.awt.Dimension(120, 23));

        tbEstado.setPreferredSize(new java.awt.Dimension(120, 23));

        tbDia.setHorizontalAlignment(javax.swing.JTextField.CENTER);
        tbDia.setText("DD");
        tbDia.setPreferredSize(new java.awt.Dimension(120, 23));

        tbPrecio.setPreferredSize(new java.awt.Dimension(120, 23));

        bAñadir.setBackground(new java.awt.Color(255, 255, 255));
        bAñadir.setText("Añadir");
        bAñadir.setBorderPainted(false);
        bAñadir.setFocusPainted(false);
        bAñadir.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                bAñadirActionPerformed(evt);
            }
        });

        lstVentas.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null}
            },
            new String [] {
                "id", "N", "Tipo", "Fecha Fin", "Puja Max."
            }
        ) {
            Class[] types = new Class [] {
                java.lang.Integer.class, java.lang.Integer.class, java.lang.String.class, java.lang.Integer.class, java.lang.Integer.class
            };
            boolean[] canEdit = new boolean [] {
                false, false, false, false, false
            };

            public Class getColumnClass(int columnIndex) {
                return types [columnIndex];
            }

            public boolean isCellEditable(int rowIndex, int columnIndex) {
                return canEdit [columnIndex];
            }
        });
        lstVentas.setGridColor(new java.awt.Color(255, 255, 255));
        lstVentas.setMaximumSize(new java.awt.Dimension(410, 233));
        lstVentas.setMinimumSize(new java.awt.Dimension(410, 233));
        lstVentas.setPreferredSize(new java.awt.Dimension(410, 233));
        jScrollPane1.setViewportView(lstVentas);
        if (lstVentas.getColumnModel().getColumnCount() > 0) {
            lstVentas.getColumnModel().getColumn(0).setPreferredWidth(5);
            lstVentas.getColumnModel().getColumn(1).setResizable(false);
            lstVentas.getColumnModel().getColumn(1).setPreferredWidth(5);
            lstVentas.getColumnModel().getColumn(2).setResizable(false);
            lstVentas.getColumnModel().getColumn(2).setPreferredWidth(30);
            lstVentas.getColumnModel().getColumn(3).setResizable(false);
            lstVentas.getColumnModel().getColumn(3).setPreferredWidth(130);
            lstVentas.getColumnModel().getColumn(4).setPreferredWidth(30);
        }

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

        jLabel13.setFont(new java.awt.Font("Tahoma", 0, 14)); // NOI18N
        jLabel13.setText("€");

        btnVender.setText("Vender");
        btnVender.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnVenderActionPerformed(evt);
            }
        });

        btnCancelar.setText("Cancelar");
        btnCancelar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnCancelarActionPerformed(evt);
            }
        });

        btEditar.setText("Editar");
        btEditar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btEditarActionPerformed(evt);
            }
        });

        jLabel23.setFont(new java.awt.Font("Segoe UI", 0, 12)); // NOI18N
        jLabel23.setText("Negociado");

        comboNegociado.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "Automático", "Manual" }));

        javax.swing.GroupLayout pVentasLayout = new javax.swing.GroupLayout(pVentas);
        pVentas.setLayout(pVentasLayout);
        pVentasLayout.setHorizontalGroup(
            pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(pVentasLayout.createSequentialGroup()
                .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(pVentasLayout.createSequentialGroup()
                        .addGap(140, 140, 140)
                        .addComponent(jLabel4)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, pVentasLayout.createSequentialGroup()
                        .addContainerGap(33, Short.MAX_VALUE)
                        .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jLabel9)
                            .addComponent(jLabel8)
                            .addComponent(jLabel7)
                            .addComponent(jLabel6)
                            .addComponent(jLabel2)
                            .addComponent(jLabel10)
                            .addComponent(bAñadir)
                            .addComponent(jLabel23))
                        .addGap(68, 68, 68)
                        .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                            .addGroup(pVentasLayout.createSequentialGroup()
                                .addComponent(tbPrecio, javax.swing.GroupLayout.PREFERRED_SIZE, 59, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(jLabel13))
                            .addGroup(pVentasLayout.createSequentialGroup()
                                .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING, false)
                                    .addGroup(javax.swing.GroupLayout.Alignment.LEADING, pVentasLayout.createSequentialGroup()
                                        .addComponent(tbDia, javax.swing.GroupLayout.PREFERRED_SIZE, 29, javax.swing.GroupLayout.PREFERRED_SIZE)
                                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                        .addComponent(tbMes, javax.swing.GroupLayout.PREFERRED_SIZE, 29, javax.swing.GroupLayout.PREFERRED_SIZE)
                                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                        .addComponent(tbAnyo, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                                    .addGroup(javax.swing.GroupLayout.Alignment.LEADING, pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                                        .addComponent(tbTipo, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                                        .addComponent(tbAutor, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                                        .addComponent(tbAño, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                                        .addComponent(tbEstado, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)))
                                .addGap(37, 37, 37)
                                .addComponent(tbHora, javax.swing.GroupLayout.PREFERRED_SIZE, 41, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(jLabel12)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(tbMinuto, javax.swing.GroupLayout.PREFERRED_SIZE, 41, javax.swing.GroupLayout.PREFERRED_SIZE))
                            .addComponent(comboNegociado, javax.swing.GroupLayout.PREFERRED_SIZE, 84, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(lbMessage, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)))
                .addComponent(jSeparator1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, pVentasLayout.createSequentialGroup()
                        .addGap(142, 142, 142)
                        .addComponent(jLabel5)
                        .addGap(166, 166, 166))
                    .addGroup(pVentasLayout.createSequentialGroup()
                        .addGap(18, 18, 18)
                        .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 479, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addGroup(pVentasLayout.createSequentialGroup()
                                .addComponent(btnVender)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                .addComponent(btnCancelar)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                .addComponent(btEditar)))
                        .addContainerGap(20, Short.MAX_VALUE))))
        );
        pVentasLayout.setVerticalGroup(
            pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(pVentasLayout.createSequentialGroup()
                .addGap(52, 52, 52)
                .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addGroup(pVentasLayout.createSequentialGroup()
                        .addComponent(jLabel4)
                        .addGap(18, 18, 18)
                        .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(tbTipo, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel2))
                        .addGap(18, 18, 18)
                        .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(tbAutor, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel6))
                        .addGap(18, 18, 18)
                        .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(tbAño, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel7))
                        .addGap(18, 18, 18)
                        .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(tbEstado, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel8))
                        .addGap(18, 18, 18)
                        .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(tbDia, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel9)
                            .addComponent(tbMes, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(tbAnyo, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(tbMinuto, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(tbHora, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel12))
                        .addGap(18, 18, 18)
                        .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(tbPrecio, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel10)
                            .addComponent(jLabel13))
                        .addGap(18, 18, 18)
                        .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel23)
                            .addComponent(comboNegociado, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(bAñadir)
                            .addComponent(lbMessage)))
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, pVentasLayout.createSequentialGroup()
                        .addComponent(jLabel5)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 238, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addGroup(pVentasLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(btnVender)
                            .addComponent(btnCancelar)
                            .addComponent(btEditar)))
                    .addComponent(jSeparator1, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.PREFERRED_SIZE, 349, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap(47, Short.MAX_VALUE))
        );

        stkTest.add(pVentas, "cVentas");

        jLabel16.setFont(new java.awt.Font("Segoe UI", 1, 20)); // NOI18N
        jLabel16.setText("Perfil");

        jLabel17.setFont(new java.awt.Font("Segoe UI", 1, 14)); // NOI18N
        jLabel17.setText("Datos");

        jLabel18.setFont(new java.awt.Font("Segoe UI", 1, 14)); // NOI18N
        jLabel18.setText("Cambiar password");

        jLabel19.setText("Nombre:");

        jLabel20.setText("e-Mail:");

        jLabel21.setText("Nuevo password 1:");

        jLabel22.setText("Nuevo password 2:");

        btConfirmar.setText("Confirmar");
        btConfirmar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btConfirmarActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout pPerfilLayout = new javax.swing.GroupLayout(pPerfil);
        pPerfil.setLayout(pPerfilLayout);
        pPerfilLayout.setHorizontalGroup(
            pPerfilLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(pPerfilLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(pPerfilLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(pPerfilLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                        .addComponent(jLabel17)
                        .addComponent(jLabel16))
                    .addGroup(pPerfilLayout.createSequentialGroup()
                        .addGap(13, 13, 13)
                        .addGroup(pPerfilLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(btConfirmar)
                            .addGroup(pPerfilLayout.createSequentialGroup()
                                .addGroup(pPerfilLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addComponent(jLabel19)
                                    .addComponent(jLabel20))
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                .addGroup(pPerfilLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addComponent(txtMail, javax.swing.GroupLayout.PREFERRED_SIZE, 340, javax.swing.GroupLayout.PREFERRED_SIZE)
                                    .addComponent(txtNombre, javax.swing.GroupLayout.PREFERRED_SIZE, 340, javax.swing.GroupLayout.PREFERRED_SIZE)))
                            .addComponent(lbPMessage, javax.swing.GroupLayout.PREFERRED_SIZE, 230, javax.swing.GroupLayout.PREFERRED_SIZE))))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 162, Short.MAX_VALUE)
                .addGroup(pPerfilLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jLabel18)
                    .addGroup(pPerfilLayout.createSequentialGroup()
                        .addComponent(jLabel22)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(jPasswordField2))
                    .addGroup(pPerfilLayout.createSequentialGroup()
                        .addComponent(jLabel21)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(jPasswordField1, javax.swing.GroupLayout.PREFERRED_SIZE, 250, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addGap(91, 91, 91))
        );
        pPerfilLayout.setVerticalGroup(
            pPerfilLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(pPerfilLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel16)
                .addGap(27, 27, 27)
                .addGroup(pPerfilLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel17)
                    .addComponent(jLabel18))
                .addGap(23, 23, 23)
                .addGroup(pPerfilLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel19)
                    .addComponent(jLabel21)
                    .addComponent(txtNombre, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jPasswordField1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(20, 20, 20)
                .addGroup(pPerfilLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel20)
                    .addComponent(jLabel22)
                    .addComponent(txtMail, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jPasswordField2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(18, 18, 18)
                .addComponent(lbPMessage, javax.swing.GroupLayout.PREFERRED_SIZE, 25, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(btConfirmar)
                .addContainerGap(196, Short.MAX_VALUE))
        );

        stkTest.add(pPerfil, "cPerfil");

        btnVentas.setText("Ventas");
        btnVentas.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnVentasActionPerformed(evt);
            }
        });

        jButton2.setText("Lista de Pujas");
        jButton2.setFocusTraversalPolicyProvider(true);
        jButton2.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton2ActionPerformed(evt);
            }
        });

        jbHistorial.setText("Historial");
        jbHistorial.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbHistorialActionPerformed(evt);
            }
        });

        jLabel1.setFont(new java.awt.Font("Yu Gothic UI Light", 1, 18)); // NOI18N
        jLabel1.setText("Galería de Arte");

        txtEvent.setEditable(false);
        txtEvent.setColumns(20);
        txtEvent.setRows(5);
        jScrollPane2.setViewportView(txtEvent);

        jPerfil.setText("Perfil");
        jPerfil.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jPerfilActionPerformed(evt);
            }
        });

        lblName.setFont(new java.awt.Font("Tahoma", 1, 12)); // NOI18N

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(stkTest, javax.swing.GroupLayout.PREFERRED_SIZE, 1020, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                    .addGroup(layout.createSequentialGroup()
                        .addGap(39, 39, 39)
                        .addComponent(jLabel1)
                        .addGap(42, 42, 42)
                        .addComponent(btnVentas, javax.swing.GroupLayout.PREFERRED_SIZE, 136, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(jButton2, javax.swing.GroupLayout.PREFERRED_SIZE, 125, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(jbHistorial, javax.swing.GroupLayout.PREFERRED_SIZE, 133, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(jPerfil, javax.swing.GroupLayout.PREFERRED_SIZE, 133, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(lblName, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addGap(18, 18, 18))))
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(jScrollPane2, javax.swing.GroupLayout.PREFERRED_SIZE, 1020, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jButton2, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(jPerfil, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(jbHistorial, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(btnVentas, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED))
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                        .addGap(0, 0, Short.MAX_VALUE)
                        .addComponent(lblName, javax.swing.GroupLayout.PREFERRED_SIZE, 24, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(26, 26, 26))
                    .addGroup(layout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(jLabel1)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))
                .addComponent(stkTest, javax.swing.GroupLayout.PREFERRED_SIZE, 448, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jScrollPane2, javax.swing.GroupLayout.PREFERRED_SIZE, 133, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void btnVentasActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnVentasActionPerformed
        CardLayout card = (CardLayout)stkTest.getLayout();
        card.show(stkTest, "cVentas");
    }//GEN-LAST:event_btnVentasActionPerformed

    private void jButton2ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton2ActionPerformed
        CardLayout card = (CardLayout)stkTest.getLayout();
        card.show(stkTest, "cPujas");
        
    
    }//GEN-LAST:event_jButton2ActionPerformed

    private void btnPujaActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnPujaActionPerformed
        try {
            String jsonPuja = "";
            Puja p = new Puja();
            p.pujadorId = user.id;
            int row = lstPujas.getSelectedRow();
            if(row >= 0 && txtPuja.getText() != ""){
                p.ventaId = (int)lstPujas.getValueAt(row, 0);
                p.cantidad = Integer.parseInt(txtPuja.getText());
                Gson gson = new Gson();
                jsonPuja = gson.toJson(p);

                ActiveMQConnectionFactory connectionFactory = new ActiveMQConnectionFactory("tcp://192.168.2.16:61616");
                Connection connection = connectionFactory.createConnection();
                connection.start();
                Session session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
                Destination destination = session.createQueue("Pujas");
                MessageProducer producer = session.createProducer(destination);
                producer.setDeliveryMode(DeliveryMode.NON_PERSISTENT);
                TextMessage message = session.createTextMessage(jsonPuja);
                producer.send(message);
                session.close();
                connection.close();
            }
        } catch (JMSException ex) {
            Logger.getLogger(MainWindow.class.getName()).log(Level.SEVERE, null, ex);
        }
    }//GEN-LAST:event_btnPujaActionPerformed

    private void btnCancelarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnCancelarActionPerformed
        try {
            URL u = new URL(new Juddi().getServiceUrl("Ventas"));
            Ventas serv = new Ventas(u);
            int row = lstVentas.getSelectedRow();
            int id = (int)lstVentas.getValueAt(row, 0);
            serv.getVentasSoap().finalizarVenta(id, 2, 0);
        } catch (MalformedURLException ex) {
            Logger.getLogger(MainWindow.class.getName()).log(Level.SEVERE, null, ex);
        }
    }//GEN-LAST:event_btnCancelarActionPerformed

    private void btnVenderActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnVenderActionPerformed
        try {
            URL u = new URL(new Juddi().getServiceUrl("Ventas"));
            Ventas serv = new Ventas(u);
            int row = lstVentas.getSelectedRow();
            int id = (int)lstVentas.getValueAt(row, 0);
            int negociado = (int)lstVentas.getValueAt(row, 1);
            if(negociado == 2){
                VentanaPujas m = new VentanaPujas();
                m.setId(id);
                m.obtenerPujas();
                m.setVisible(true);
            }else{
                serv.getVentasSoap().finalizarVenta(id, 1, 0);
            }
            
        } catch (MalformedURLException ex) {
            Logger.getLogger(MainWindow.class.getName()).log(Level.SEVERE, null, ex);
        }
    }//GEN-LAST:event_btnVenderActionPerformed

    private void tbMesActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_tbMesActionPerformed
        // TODO add your handling code here:
    }//GEN-LAST:event_tbMesActionPerformed

    private void bAñadirActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_bAñadirActionPerformed
        try {
            boolean valido = true;
            Venta venta = new Venta();
            Auxiliar aux = new Auxiliar();
            String jsonVenta = "";
            URL u = new URL(new Juddi().getServiceUrl("Ventas"));
            Ventas serv = new Ventas(u);
            Gson gson = new GsonBuilder().create();
            
            //Comprobaciones
            //Campos numéricos
            if(!Auxiliar.isNumeric(tbAño.getText())
                    || !Auxiliar.isNumeric(tbPrecio.getText())
                    || !Auxiliar.isNumeric(tbAnyo.getText())
                    || !Auxiliar.isNumeric(tbMes.getText())
                    || !Auxiliar.isNumeric(tbDia.getText())
                    || !Auxiliar.isNumeric(tbHora.getText())
                    || !Auxiliar.isNumeric(tbMinuto.getText())){
                valido = false;
                
                //Error, alguno de los campos no es numérico
                lbMessage.setForeground(Color.red);
                lbMessage.setText("Algún campo no contiene un valor numérico");
            }else{
                //Comprobacion de fechas
                if((Integer.parseInt(tbMes.getText()) > 0 && Integer.parseInt(tbMes.getText())<=12)
                        && (Integer.parseInt(tbDia.getText()) > 0 && Integer.parseInt(tbDia.getText()) <=31)
                        && (Integer.parseInt(tbHora.getText())>=0 && Integer.parseInt(tbHora.getText()) <24)
                        && (Integer.parseInt(tbMinuto.getText())>=0 && Integer.parseInt(tbMinuto.getText())<60)
                        ){
                        DateTime date = new DateTime(Integer.parseInt(tbAnyo.getText()), Integer.parseInt(tbMes.getText()), Integer.parseInt(tbDia.getText()), Integer.parseInt(tbHora.getText()), Integer.parseInt(tbMinuto.getText()), 0);
                        if (date.compareTo(DateTime.now())>0)
                        {
                            valido=true;
                        }else{
                            valido = false;
                            lbMessage.setForeground(Color.red);
                            lbMessage.setText("La fecha es anterior a la actual");
                        }
                }
                else{
                    valido=false;
                    lbMessage.setForeground(Color.red);
                    lbMessage.setText("Fecha no válida");
                }
            }
            
            if(valido){
                venta.autor = tbAutor.getText();
                venta.año =  Integer.parseInt(tbAño.getText());
                venta.estado = tbEstado.getText();
                venta.precio =  Integer.parseInt(tbPrecio.getText());

                DateTime date = new DateTime(Integer.parseInt(tbAnyo.getText()), Integer.parseInt(tbMes.getText()), Integer.parseInt(tbDia.getText()), Integer.parseInt(tbHora.getText()), Integer.parseInt(tbMinuto.getText()), 0);
                venta.fecha_F = date.toString();
                venta.vendedor = user.id;
                venta.tipo = tbTipo.getText();
                venta.idComprador = 0;

                int negociado = 0;
                if ("Automático".equals(comboNegociado.getSelectedItem().toString()))
                {
                    negociado = 1;
                }
                else
                {
                    negociado = 2;
                }

                venta.negociado = negociado;

                jsonVenta = gson.toJson(venta);
                serv.getVentasSoap().nuevaVenta(jsonVenta);
                lbMessage.setForeground(Color.green);
                lbMessage.setText("Venta añadida");
            }
        } catch (MalformedURLException ex) {
            Logger.getLogger(MainWindow.class.getName()).log(Level.SEVERE, null, ex);
        }

    }//GEN-LAST:event_bAñadirActionPerformed

    
    
    private void jbHistorialActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbHistorialActionPerformed
        CardLayout card = (CardLayout)stkTest.getLayout();
        card.show(stkTest, "cHistorial");
    }//GEN-LAST:event_jbHistorialActionPerformed

    private void jPerfilActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jPerfilActionPerformed
        CardLayout card = (CardLayout)stkTest.getLayout();
        card.show(stkTest, "cPerfil");
    }//GEN-LAST:event_jPerfilActionPerformed

    private void btConfirmarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btConfirmarActionPerformed
        try {
            boolean valido = true;
            Usuario u = new Usuario();
            String jsonUser = "";
            Auxiliar aux = new Auxiliar();
            u.id = user.id;
            u.Nombre = txtNombre.getText();
            u.Mail = txtMail.getText();
            u.Password = "";
            
            if(Arrays.equals(jPasswordField1.getPassword(), jPasswordField2.getPassword())){
                if (!"".equals(jPasswordField1.getPassword()) && !"".equals(jPasswordField2.getPassword())) {
                    try {
                        u.Password = aux.sha1(jPasswordField1.getText());
                    } catch (NoSuchAlgorithmException ex) {
                        Logger.getLogger(MainWindow.class.getName()).log(Level.SEVERE, null, ex);
                    }
                }
            }else{
                lbPMessage.setForeground(Color.red);
                lbPMessage.setText("Las contraseñas no coinciden");
                valido = false;
            }
            
            if ("".equals(u.Nombre))
            {
                lbPMessage.setForeground(Color.red);
                lbPMessage.setText("El campo nombre no puede estar vacio");
                valido = false;
            }

            if ("".equals(u.Mail))
            {
                lbPMessage.setForeground(Color.red);
                lbPMessage.setText("El campo e-mail no puede estar vacio");
                valido = false;
            }
            
            if(valido){
                URL url = new URL(new Juddi().getServiceUrl("Usuarios"));
                Usuarios serv = new Usuarios(url);
                Gson gson = new GsonBuilder().create();
                jsonUser = gson.toJson(u, Usuario.class);
                serv.getUsuariosSoap().updateUser(jsonUser);
                lbPMessage.setForeground(Color.green);
                lbPMessage.setText("Cambios realizados");
            }
        } catch (MalformedURLException ex) {
            Logger.getLogger(MainWindow.class.getName()).log(Level.SEVERE, null, ex);
        }
    }//GEN-LAST:event_btConfirmarActionPerformed

    private void btEditarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btEditarActionPerformed
        int row = lstVentas.getSelectedRow();
        int id = (int)lstVentas.getValueAt(row, 0);
        VentanaEdicion m = new VentanaEdicion();
        m.setId(id);
        m.setVisible(true);
    }//GEN-LAST:event_btEditarActionPerformed

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
            java.util.logging.Logger.getLogger(MainWindow.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(MainWindow.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(MainWindow.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(MainWindow.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */

    }

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton bAñadir;
    private javax.swing.JButton btConfirmar;
    private javax.swing.JButton btEditar;
    private javax.swing.JButton btnCancelar;
    private javax.swing.JButton btnPuja;
    private javax.swing.JButton btnVender;
    private javax.swing.JButton btnVentas;
    private javax.swing.JComboBox comboNegociado;
    private javax.swing.JButton jButton2;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel10;
    private javax.swing.JLabel jLabel11;
    private javax.swing.JLabel jLabel12;
    private javax.swing.JLabel jLabel13;
    private javax.swing.JLabel jLabel14;
    private javax.swing.JLabel jLabel15;
    private javax.swing.JLabel jLabel16;
    private javax.swing.JLabel jLabel17;
    private javax.swing.JLabel jLabel18;
    private javax.swing.JLabel jLabel19;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel20;
    private javax.swing.JLabel jLabel21;
    private javax.swing.JLabel jLabel22;
    private javax.swing.JLabel jLabel23;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JLabel jLabel6;
    private javax.swing.JLabel jLabel7;
    private javax.swing.JLabel jLabel8;
    private javax.swing.JLabel jLabel9;
    private javax.swing.JPasswordField jPasswordField1;
    private javax.swing.JPasswordField jPasswordField2;
    private javax.swing.JButton jPerfil;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JScrollPane jScrollPane2;
    private javax.swing.JScrollPane jScrollPane3;
    private javax.swing.JScrollPane jScrollPane4;
    private javax.swing.JSeparator jSeparator1;
    private javax.swing.JButton jbHistorial;
    private javax.swing.JLabel lbMessage;
    private javax.swing.JLabel lbPMessage;
    private javax.swing.JLabel lblName;
    private javax.swing.JTable lstHistorico;
    private javax.swing.JTable lstPujas;
    private javax.swing.JTable lstVentas;
    private javax.swing.JPanel pHistorial;
    private javax.swing.JPanel pPerfil;
    private javax.swing.JPanel pPujas;
    private javax.swing.JPanel pVentas;
    private javax.swing.JPanel stkTest;
    private javax.swing.JTextField tbAnyo;
    private javax.swing.JTextField tbAutor;
    private javax.swing.JTextField tbAño;
    private javax.swing.JTextField tbDia;
    private javax.swing.JTextField tbEstado;
    private javax.swing.JTextField tbHora;
    private javax.swing.JTextField tbMes;
    private javax.swing.JTextField tbMinuto;
    private javax.swing.JTextField tbPrecio;
    private javax.swing.JTextField tbTipo;
    private javax.swing.JTextArea txtEvent;
    private javax.swing.JTextField txtMail;
    private javax.swing.JTextField txtNombre;
    private javax.swing.JTextField txtPuja;
    // End of variables declaration//GEN-END:variables

    @Override
    public void onMessage(Message msg) {
        try {
            String messageText="";
            String topic = msg.getJMSDestination().toString();
            messageText = ((TextMessage) msg).getText();
            //Comprobamos a que Topic pertenece
            if(topic.contains("Obras")){
                onMessageObras(messageText);
            }else if(topic.contains("Pujas")){
                onMessagePujas(messageText);
            }else if(topic.contains("Ventas")){
                onMessageVentas(messageText);
            }
        } catch (JMSException ex) {
            Logger.getLogger(MainWindow.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
    private void onMessageObras(String message){
        actualizarListaVentas();
        actualizarListaPujas();
    }
    
    private void onMessagePujas(String message){
        actualizarListaVentas();
        actualizarListaPujas();
    }
        
    private void onMessageVentas(String message){
        Venta v = new Venta();
        Gson gson = new GsonBuilder().create();
        v = gson.fromJson(message, Venta.class);
        
        //Informar de venta finalizada y a quien o si ha sido cancelada
        if(v.finalizada == 2){ //Cancelada
            txtEvent.setForeground(Color.RED);
            txtEvent.append("La subasta de la obra " + v.tipo + " con ID " + v.id + " ha sido cancelada por el vendedor\r\n");
        }else if(v.finalizada == 1){ //Vendida
            if(v.comprador != null){
                txtEvent.setForeground(Color.GREEN);
                txtEvent.append("La obra " + v.tipo + " con ID " + v.id + " ha sido vendida a " + v.comprador + "\r\n");
            }else{
                txtEvent.setForeground(Color.RED);
                txtEvent.append("La subasta de la obra " + v.tipo + " con ID " + v.id + " ha quedado desierta\r\n"); 
            }
        }else if(v.finalizada ==4){
                txtEvent.setForeground(Color.GREEN);
                txtEvent.append("La obra " + v.tipo + " con ID " + v.id + " ha sido vendida a " + v.comprador + "\r\n");
        }
        
        actualizarListaPujas();
        actualizarListaVentas();
        actualizarListaHistorial();
    }
    
    public void actualizarListaPujas(){
        try {
            Venta v = new Venta();
            List<Venta> lv = new ArrayList<>();
            String jsonVentas = "";
            URL u = new URL(new Juddi().getServiceUrl("Ventas"));
            Ventas serv = new Ventas(u);
            jsonVentas = serv.getVentasSoap().getVentas(user.id);
            Gson gson = new GsonBuilder().create();
            lv = gson.fromJson(jsonVentas, new TypeToken<List<Venta>>(){}.getType());
            
            DefaultTableModel model = (DefaultTableModel) lstPujas.getModel();
            model.setRowCount(0);
            for (Venta lv1 : lv) {
                DateTime date = DateTime.parse(lv1.fecha_F, DateTimeFormat.forPattern("dd/MM/yyyy HH:mm:ss"));
                DateTimeZone localTimeZone = DateTimeZone.forID(TimeZone.getDefault().toZoneId().toString());
                DateTime utcTime = new DateTime(date, DateTimeZone.UTC);
                DateTime localTime = utcTime.withZone(localTimeZone).plusHours(2);
                model.addRow(new Object[]{lv1.id, lv1.tipo, lv1.estado, lv1.autor, localTime.toString(DateTimeFormat.forPattern("dd/MM/yyyy HH:mm:ss")), lv1.precio});
            }
        } catch (MalformedURLException ex) {
            Logger.getLogger(MainWindow.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
    public void actualizarListaVentas(){
        try {
            List<Venta> lv = new ArrayList<>();
            String jsonVentas = "";
            URL u = new URL(new Juddi().getServiceUrl("Ventas"));
            Ventas serv = new Ventas(u);
            jsonVentas = serv.getVentasSoap().getVentasActivas(user.id);
            Gson gson = new GsonBuilder().create();
            lv = gson.fromJson(jsonVentas, new TypeToken<List<Venta>>(){}.getType());
            
            DefaultTableModel model = (DefaultTableModel) lstVentas.getModel();
            model.setRowCount(0);
            for (Venta lv1 : lv) {
                DateTime date = DateTime.parse(lv1.fecha_F, DateTimeFormat.forPattern("dd/MM/yyyy HH:mm:ss"));
                DateTimeZone localTimeZone = DateTimeZone.forID(TimeZone.getDefault().toZoneId().toString());
                DateTime utcTime = new DateTime(date, DateTimeZone.UTC);
                DateTime localTime = utcTime.withZone(localTimeZone).plusHours(2);
                DateTime localDate = new org.joda.time.DateTime();
                if(localDate.compareTo(localTime)<0){
                    model.addRow(new Object[]{lv1.id, lv1.negociado, lv1.tipo, localTime.toString(DateTimeFormat.forPattern("dd/MM/yyyy HH:mm:ss")), lv1.precio});
                }else{
                    model.addRow(new Object[]{lv1.id, lv1.negociado, lv1.tipo, "FIN", lv1.precio});
                }
            }
        } catch (MalformedURLException ex) {
            Logger.getLogger(MainWindow.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
    public void actualizarListaHistorial(){
        try {
            HistoricoVentas hv = new HistoricoVentas();
            String jsonVentas = "";
            URL u = new URL(new Juddi().getServiceUrl("Historico"));
            Historico serv = new Historico(u);
            jsonVentas = serv.getHistoricoSoap().devolverTodo(user.id);
            Gson gson = new GsonBuilder().create();
            hv = gson.fromJson(jsonVentas, HistoricoVentas.class);
            
            DefaultTableModel model = (DefaultTableModel) lstHistorico.getModel();
            model.setRowCount(0);
            for (Venta lv1 : hv.ListaVentas) {
                model.addRow(new Object[]{lv1.id, lv1.tipo, lv1.estado, lv1.precio});
            }
        } catch (MalformedURLException ex) {
            Logger.getLogger(MainWindow.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}

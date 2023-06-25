<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="frmCrudUsuario.aspx.cs" Inherits="Presentacion.frmCrudUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    <%#  GetTitulo()%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row ">
            <div class="col-lg-12 col-md-12">
                <div class="card">
                    <div class="card-header card-header-text">
                        <asp:Label ID="lblTitulo" CssClass="card-title" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="card-content">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb d-flex justify-content-end">
                                <li class="breadcrumb-item"><a href="#"><i class="fa-solid fa-home"></i>
                                    Inicio</a></li>
                                <li class="breadcrumb-item"><a href="#"><i class="fa-solid fa-users"></i>
                                    Usuarios</a></li>
                                <li class="breadcrumb-item active" aria-current="page"><%# GetTitulo() %></li>
                            </ol>
                        </nav>

                        <div class="row my-2">
                            <div class="col-md-6">
                                <label class="form-label">Usuario</label>
                                <asp:TextBox ID="txtUsuario" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Nombre</label>
                                <asp:TextBox ID="txtNombre" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row my-2">
                            <div class="col-md-6">
                                <label class="form-label">Apellido</label>
                                <asp:TextBox ID="txtApellido" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Correo electrónico</label>
                                <asp:TextBox ID="txtEmail" class="form-control" runat="server" TextMode="Email"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row my-2">
                            <div class="col-md-6">
                                 <label class="form-label">Password</label>
                                 <asp:TextBox ID="txtPassword" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                 <label class="form-label">Confirmar password</label>
                                 <asp:TextBox ID="txtVerification" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Tipo de usuario</label>
                            <asp:DropDownList ID="cmbTipoUsuario" CssClass="form-select" runat="server"></asp:DropDownList>
                        </div>

                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary me-2" Text="Crear usuario" Visible="False" OnClick="btnGuardar_Click" />
                            <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-primary me-2" Text="Actualizar usuario" Visible="False" OnClick="btnActualizar_Click" />
                            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-danger" Text="Volver" OnClick="btnVolver_Click" />
                        </div>

                    </div>
                    <asp:Literal ID="ErrorLiteral" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

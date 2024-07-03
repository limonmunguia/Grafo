<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Grafo.aspx.cs" Inherits="WebGrafo.Grafo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Grafo</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/vis-network/9.1.9/dist/dist/vis-network.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/vis-network/9.1.9/standalone/umd/vis-network.min.js" ></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous"/>
    <script type="text/javascript">
        function renderGraph(graphData) {
            var nodes = new vis.DataSet(graphData.nodes);
            var edges = new vis.DataSet(graphData.edges);
            var container = document.getElementById('network');
            var data = {
                nodes: nodes,
                edges: edges
            };
            var options = {};
            var network = new vis.Network(container, data, options);
        }
    </script>
</head>
<body> 
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-6">
                    <asp:UpdatePanel ID="UpdatePanelCiudad" runat="server">
                        <ContentTemplate>
                            <h1 class="mb-4">Ingresar Ciudad</h1>
                            <div class="mb-3">
                                <label for="txtCiudad" class="form-label">Nombre Ciudad</label>
                                <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="txtSuperficie" class="form-label">Superficie</label>
                                <asp:TextBox ID="txtSuperficie" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <label for="txtTHabit" class="form-label">Total Habitante</label>
                                <asp:TextBox ID="txtTHabit" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <asp:Button ID="btnAddCity" runat="server" Text="Agregar Ciudad" CssClass="btn btn-primary mb-3" OnClick="btnAddCity_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <hr class="my-5" />

                    <asp:UpdatePanel ID="UpdatePanelCamino" runat="server">
                        <ContentTemplate>
                            <h1 class="mb-4">Ingresar Camino</h1>

                            <h3 class="mb-3">Selecciona la ciudad:</h3>
                            <div class="row mb-3">
                                <div class="col">
                                    <label for="ddlOrig" class="form-label">Origen</label>
                                    <asp:DropDownList ID="ddlOrig" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlOrig_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col">
                                    <label for="ddlDest" class="form-label">Destino</label>
                                    <asp:DropDownList ID="ddlDest" runat="server" CssClass="form-select"></asp:DropDownList>
                                </div>
                                <div class="mb-3">
                                <label for="txtPeso" class="form-label">Distancia</label>
                                <asp:TextBox ID="txtPeso" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            </div>
                            <asp:Button ID="btnAddCamino" runat="server" Text="Añadir Camino" CssClass="btn btn-primary mb-3" OnClick="btnAddCamino_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <hr class="my-5" />

                    <asp:UpdatePanel ID="UpdatePanelCaminos" runat="server">
                        <ContentTemplate>
                            <h2 class="mb-4">Mostrar Caminos de una ciudad</h2>
                            <div class="mb-3">
                                <label for="ddlCityCaminos" class="form-label">Seleccionar Ciudad</label>
                                <asp:DropDownList ID="ddlCityCaminos" runat="server" CssClass="form-select"></asp:DropDownList>
                            </div>
                            <asp:Button ID="btnMostrarArista" runat="server" Text="Mostrar Caminos" CssClass="btn btn-primary mb-3" OnClick="btnMostrarArista_Click" />
                            <div class="mb-3">
                                <label for="ddlAristas" class="form-label">Caminos</label>
                                <asp:DropDownList ID="ddlAristas" runat="server" CssClass="form-select"></asp:DropDownList>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <hr class="my-5" />

                    <asp:UpdatePanel ID="UpdatePanelRecorridos" runat="server">
                        <ContentTemplate>
                            <h1 class="mb-4">Recorridos</h1>
                            <div class="mb-3">
                                <label for="ddlCiudadIIni" class="form-label">Seleccionar nodo de inicio</label>
                                <asp:DropDownList ID="ddlCiudadIIni" runat="server" CssClass="form-select"></asp:DropDownList>
                            </div>
                            <div class="row mb-3">
                                <div class="col">
                                    <asp:Button ID="btnDFS" runat="server" Text="DFS" CssClass="btn btn-secondary" OnClick="btnDFS_Click" />
                                </div>
                                <div class="col">
                                    <asp:Button ID="btnBFS" runat="server" Text="BFS" CssClass="btn btn-secondary" OnClick="btnBFS_Click" />
                                </div>
                            </div>
                            <asp:GridView ID="gv1" runat="server" CssClass="table table-striped"></asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <hr class="my-5" />

                    <asp:UpdatePanel ID="UpdatePanelCaminoCorto" runat="server">
                        <ContentTemplate>
                            <h1 class="mb-4">Encontrar camino más corto</h1>
                            <div class="row mb-3">
                                <div class="col">
                                    <label for="ddlDijkstraOri" class="form-label">Origen</label>
                                    <asp:DropDownList ID="ddlDijkstraOri" runat="server" CssClass="form-select"></asp:DropDownList>
                                </div>
                                <div class="col">
                                    <label for="ddlDijkstraDest" class="form-label">Destino</label>
                                    <asp:DropDownList ID="ddlDijkstraDest" runat="server" CssClass="form-select"></asp:DropDownList>
                                </div>
                            </div>
                            <asp:Button ID="btnDijkstra" runat="server" Text="Encontrar" CssClass="btn btn-primary mb-3" OnClick="btnDijkstra_Click" />
                            <asp:GridView ID="gvDijkstra" runat="server" CssClass="table table-striped"></asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <hr />
                    <asp:UpdatePanel ID="UpdatePanelBusqueda" runat="server">
                        <ContentTemplate>
                            <h1 class="mb-4">Busqueda Topólogica de Kahn</h1><br />
                            <asp:Button ID="btnBusqueda" runat="server" Text="Busqueda"  CssClass="btn btn-primary mb-3" OnClick="btnBusqueda_Click"/>

                            <asp:GridView ID="gvbusquedatopo" runat="server" CssClass="table table-striped"></asp:GridView>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="col-md-6">
                    <h1 class="mb-4">Visualización del Grafo</h1><br />
                    <asp:Button ID="btnGraficar" runat="server" Text="Graficar" CssClass="btn btn-primary mb-3" OnClick="btnGraficar_Click"/>
                    
                    <div id="network" class="border p-3" style="height: 600px;">
                        <!-- Aquí se dibujará el grafo -->
                    </div>
                </div>
            </div>
        </div>
        
    </form> 
    <script src="https://code.jquery.com/jquery-3.7.1.js" integrity="sha256-eKhayi8LEQwp4NKxN+CfCh+3qOVUtJn3QNZ0TciWLP4=" crossorigin="anonymous"></script>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    
    </body>

</html>
